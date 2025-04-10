using System.Collections.Generic;
using UnityEngine;

public enum ePartType
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
    public GameObject[] m_weapons;
    public Character character;

    // 현재 장착된 파츠를 저장 (ePartType별)
    private Dictionary<ePartType, GameObject> currentEquipped = new Dictionary<ePartType, GameObject>();


    public Dictionary<ePartType, List<GameObject>> partsByType = new Dictionary<ePartType, List<GameObject>>();
    private void InitializeParts()
    {
        // 배열을 Dictionary에 등록합니다.
        partsByType[ePartType.Head] = new List<GameObject>(m_heads);
        partsByType[ePartType.Body] = new List<GameObject>(m_bodys);
        partsByType[ePartType.Weapon] = new List<GameObject>(m_weapons);

        // 각 파츠 타입마다 기본 파츠를 활성화 (있다면)
        foreach (var kvp in partsByType)
        {
            ePartType partType = kvp.Key;
            List<GameObject> parts = kvp.Value;

            if (parts.Count > 0)
            {
                EquipPart(partType, 0);
            }
            else
            {
                Debug.LogWarning($"[{partType}] 파츠 목록이 비어있습니다.");
            }
        }
    }



    private void Awake()
    {
        character = GetComponent<Character>();
        InitializeParts();
    }


    #region 파츠

    /// <summary>
    /// 특정 파츠 타입의 파츠 중 index에 해당하는 파츠를 장착합니다.
    /// </summary>
    /// <param name="partType">장착할 파츠 타입</param>
    /// <param name="index">해당 타입 내에서 선택할 인덱스</param>
    public void EquipPart(ePartType partType, int index)
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
    private void SetActiveExclusive(ePartType partType, int activeIndex)
    {
        List<GameObject> parts = partsByType[partType];

        for (int i = 0; i < parts.Count; i++)
        {
            bool shouldActivate = (i == activeIndex);
            
            parts[i].SetActive(shouldActivate);

            if (shouldActivate)
            {
                // 현재 장착된 파츠 업데이트
                if (currentEquipped.ContainsKey(partType))
                {
                    currentEquipped[partType] = parts[i];
                }
                else
                {
                    currentEquipped.Add(partType, parts[i]);
                }
            }
        }
    }

    #endregion



}
