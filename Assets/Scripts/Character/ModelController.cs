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


    //파츠바디 타입에는 무기를 포함하지않는다.
    public Dictionary<eEuipmentType, List<GameObject>> partsByType = new Dictionary<eEuipmentType, List<GameObject>>();
    private void InitializeParts()
    {
        // 배열을 Dictionary에 등록합니다.
        partsByType[eEuipmentType.Head] = new List<GameObject>(m_heads);
        partsByType[eEuipmentType.Body] = new List<GameObject>(m_bodys);

        // 각 파츠 타입마다 기본 파츠를 활성화 (있다면)
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
                //Debug.LogWarning($"[{partType}] 파츠 목록이 비어있습니다.");
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


    #region 파츠

    /// <summary>
    /// 특정 파츠 타입의 파츠 중 index에 해당하는 파츠를 장착합니다.
    /// </summary>
    /// <param name="partType">장착할 파츠 타입</param>
    /// <param name="index">해당 타입 내에서 선택할 인덱스</param>
    public void EquipPart(eEuipmentType partType, int index)
    {
        if (!partsByType.ContainsKey(partType))
        {
            Debug.LogWarning($"장착할 수 없는 파츠 타입: {partType}");
            return;
        }

        List<GameObject> parts = partsByType[partType];
        if (index < 0 || index >= parts.Count)
        {
            Debug.LogWarning($"파츠 타입 [{partType}]에 대해 인덱스 {index}가 범위를 벗어났습니다.");
            return;
        }

        // 해당 파츠 타입 내에서 하나의 오브젝트만 활성화하도록 처리
        SetActiveExclusive(partType, index);
    }

    /// <summary>
    /// 같은 파츠 타입 내에서 지정한 인덱스의 오브젝트만 활성화하고, 나머지는 비활성화합니다.
    /// </summary>
    /// <param name="partType">파츠 타입</param>
    /// <param name="activeIndex">활성화할 오브젝트의 인덱스</param>
    public void SetActiveExclusive(eEuipmentType partType, int activeIndex)
    {
        List<GameObject> parts = partsByType[partType];

        for (int i = 0; i < parts.Count; i++)
        {
            bool shouldActivate = (i == activeIndex);
            
            parts[i].SetActive(shouldActivate);

            // 활성화
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

            //활성화된 현제 무기
            if (isActive)
            {                
                character.SetWeaponEffect(dicWeapons[type]);
            }
        }
    }


    #endregion



}
