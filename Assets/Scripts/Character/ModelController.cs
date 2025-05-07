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
    public GameObject[] m_heads;
    public GameObject[] m_bodys;
   
    public WeaponController[] m_weapons;

    public Dictionary<eWeaponType, WeaponController> dicWeapons;

    public Character character;

    public Rig HeadAnim;
    public Rig HandRighAnim;
    public Rig HandLeftAnim;
    public Rig LowerarmLeftAnim;
    public Rig LowerarmRightAnim;


    //�����ٵ� Ÿ�Կ��� ���⸦ ���������ʴ´�.
    public Dictionary<eEuipmentType, List<GameObject>> partsByType = new Dictionary<eEuipmentType, List<GameObject>>();
    private void InitializeParts()
    {
        // �迭�� Dictionary�� ����մϴ�.
        partsByType[eEuipmentType.Head] = new List<GameObject>(m_heads);
        partsByType[eEuipmentType.Body] = new List<GameObject>(m_bodys);

        // �� ���� Ÿ�Ը��� �⺻ ������ Ȱ��ȭ (�ִٸ�)
        foreach (var kvp in partsByType)
        {
            eEuipmentType partType = kvp.Key;
            List<GameObject> parts = kvp.Value;

            if (parts.Count > 0)
            {
                EquipPart(partType, 0);
            }
            else
            {
                //Debug.LogWarning($"[{partType}] ���� ����� ����ֽ��ϴ�.");
            }
        }
        dicWeapons = new Dictionary<eWeaponType, WeaponController>();
        foreach (var weapon in m_weapons)
        {
            dicWeapons.Add(weapon.eWeaponType, weapon);
        }

        ScanForTargets.OnSetLockOnTarget += OnSetLockOnTarget;
    }

    public WeightedTransform posxxx;

    public void OnSetLockOnTarget(Transform Target)
    {

    }

    private void AimingPosesRigging(Rig weiponAimLayer, Transform Target)
    {


        weiponAimLayer.weight = 1;
        GameObject child = weiponAimLayer.transform.GetChild(0).gameObject;
        MultiAimConstraint childCompo =  child.GetComponent<MultiAimConstraint>();

        childCompo.weight = 1;
        posxxx = childCompo.data.sourceObjects[0];
        
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
    public void EquipPart(eEuipmentType partType, int index)
    {
        if (!partsByType.ContainsKey(partType))
        {
            Debug.LogWarning($"������ �� ���� ���� Ÿ��: {partType}");
            return;
        }

        List<GameObject> parts = partsByType[partType];
        if (index < 0 || index >= parts.Count)
        {
            Debug.LogWarning($"���� Ÿ�� [{partType}]�� ���� �ε��� {index}�� ������ ������ϴ�.");
            return;
        }

        // �ش� ���� Ÿ�� ������ �ϳ��� ������Ʈ�� Ȱ��ȭ�ϵ��� ó��
        SetActiveExclusive(partType, index);
    }

    /// <summary>
    /// ���� ���� Ÿ�� ������ ������ �ε����� ������Ʈ�� Ȱ��ȭ�ϰ�, �������� ��Ȱ��ȭ�մϴ�.
    /// </summary>
    /// <param name="partType">���� Ÿ��</param>
    /// <param name="activeIndex">Ȱ��ȭ�� ������Ʈ�� �ε���</param>
    public void SetActiveExclusive(eEuipmentType partType, int activeIndex)
    {
        List<GameObject> parts = partsByType[partType];

        for (int i = 0; i < parts.Count; i++)
        {
            bool shouldActivate = (i == activeIndex);
            
            parts[i].SetActive(shouldActivate);

            // Ȱ��ȭ
            if (shouldActivate)
            {
                
            }
        }
    }

    public void SetWeaponByIndex(eWeaponType type)
    {      
        foreach(var weapon in dicWeapons)
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


    #endregion



}
