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


    //�����ٵ� Ÿ�Կ��� ���⸦ ���������ʴ´�.
    public Dictionary<eEuipmentType, GameObject> partsByType = new Dictionary<eEuipmentType, GameObject>();
    protected virtual void InitializeParts()
    {
        // �迭�� Dictionary�� ����մϴ�.
        partsByType[eEuipmentType.Head] = m_head;
        partsByType[eEuipmentType.Body] = m_body;

        // �� ���� Ÿ�Ը��� �⺻ ������ Ȱ��ȭ (�ִٸ�)
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


    #region ����

    /// <summary>
    /// Ư�� ���� Ÿ���� ���� �� index�� �ش��ϴ� ������ �����մϴ�.
    /// </summary>
    /// <param name="partType">������ ���� Ÿ��</param>
    /// <param name="index">�ش� Ÿ�� ������ ������ �ε���</param>
    public void EquipPart(eEuipmentType partType)
    {
        if (!partsByType.ContainsKey(partType))
        {
            Debug.LogWarning($"������ �� ���� ���� Ÿ��: {partType}");
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

            //Ȱ��ȭ�� ���� ����
            if (isActive)
            {
                character.SetWeaponEffect(dicWeapons[type]);
            }
        }
    }


}
