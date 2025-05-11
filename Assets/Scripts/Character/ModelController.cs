using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public enum eEuipmentType
{
    None = 0,
    Head = 1,
    Body = 2,
    Weapon = 3,
}



public class ModelController : MonoBehaviour
{
    public GameObject m_head;
    public GameObject m_body;
   
    public WeaponController[] m_weapons;
    public Dictionary<eWeaponType, WeaponController> dicWeapons;

    public Character character;


    //파츠바디 타입에는 무기를 포함하지않는다.
    public Dictionary<eEuipmentType, GameObject> partsByType = new Dictionary<eEuipmentType, GameObject>();
    protected virtual void InitializeParts()
    {
        // 배열을 Dictionary에 등록합니다.
        partsByType[eEuipmentType.Head] = m_head;
        partsByType[eEuipmentType.Body] = m_body;

        // 각 파츠 타입마다 기본 파츠를 활성화 (있다면)
        foreach (var kvp in partsByType)
        {
            eEuipmentType partType = kvp.Key;
            GameObject parts = kvp.Value;
            EquipPart(partType);
        }
    }




    private void Awake()
    {        
        InitializeParts();
    }


    #region 파츠

    /// <summary>
    /// 특정 파츠 타입의 파츠 중 index에 해당하는 파츠를 장착합니다.
    /// </summary>
    /// <param name="partType">장착할 파츠 타입</param>
    /// <param name="index">해당 타입 내에서 선택할 인덱스</param>
    public void EquipPart(eEuipmentType partType)
    {
        if (!partsByType.ContainsKey(partType))
        {
            Debug.LogWarning($"장착할 수 없는 파츠 타입: {partType}");
            return;
        }

        GameObject part = partsByType[partType];
        if(part == null)
        {
            Debug.LogWarning("Not Found");
        }
    }

    public void SetActiveExclusive(eEuipmentType partType, Mesh mesh)
    {
        GameObject part = partsByType[partType];
        var render = part.GetComponent<SkinnedMeshRenderer>();
        render.sharedMesh = mesh;

    }



    #endregion


    public virtual void SetWeaponByIndex(eWeaponType type)
    {
        foreach (var weapon in dicWeapons)
        {
            bool isActive = type == weapon.Key;
            dicWeapons[weapon.Key].gameObject.SetActive(isActive);

            //활성화된 현제 무기
            if (isActive)
            {
                character.SetWeaponEffect(dicWeapons[type]);
            }
        }
    }


}
