using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Rendering;

public enum eEuipmentType
{
    None = 0,
    Head = 1,
    Body = 2,
    Weapon = 3,
}



public class ModelController : AnimationControllerBase
{
    public SkinnedMeshRenderer m_head;
    public SkinnedMeshRenderer m_body;
   
    public WeaponController[] m_weapons;
    public Dictionary<eEuipmentType, SkinnedMeshRenderer> parts;
    public Dictionary<eWeaponType, WeaponController> dicWeapons;

    public Character character;

    protected virtual void InitializeParts()
    {
        parts = new Dictionary<eEuipmentType, SkinnedMeshRenderer> ();
        parts.Add(eEuipmentType.Body, m_body);
        parts.Add(eEuipmentType.Head, m_head);
    }




    protected override void Awake()
    {        
        base.Awake();
        InitializeParts();
    }


    #region 颇明


    public void SetActiveExclusive(eEuipmentType partType, GameObject model)
    {
        MeshFilter modelFilter = model.GetComponent<MeshFilter>();
        if (modelFilter == null)
        {
            Debug.LogWarning("SkinnedMeshRenderer not found on model");
            return;
        }

        SkinnedMeshRenderer targetRenderer = parts[partType];
        if (targetRenderer == null)
        {
            Debug.LogWarning($"SkinnedMeshRenderer not found in parts[{partType}]");
            return;
        }

        targetRenderer.sharedMesh = modelFilter.sharedMesh;
    }



    #endregion


    public virtual void SetWeaponByIndex(eWeaponType type)
    {
        foreach (var weapon in dicWeapons)
        {
            bool isActive = type == weapon.Key;
            dicWeapons[weapon.Key].gameObject.SetActive(isActive);

            //劝己拳等 泅力 公扁
            if (isActive)
            {
                character.SetWeaponEffect(dicWeapons[type]);
            }
        }
    }

    public void OnFireAnimationComplete()
    {
    }

    public Action FireAnimationApply;
    public void OnFireAnimationApply()
    {
        FireAnimationApply?.Invoke();
    }

}
