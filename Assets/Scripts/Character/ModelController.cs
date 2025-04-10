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

    // ���� ������ ������ ���� (ePartType��)
    private Dictionary<ePartType, GameObject> currentEquipped = new Dictionary<ePartType, GameObject>();


    public Dictionary<ePartType, List<GameObject>> partsByType = new Dictionary<ePartType, List<GameObject>>();
    private void InitializeParts()
    {
        // �迭�� Dictionary�� ����մϴ�.
        partsByType[ePartType.Head] = new List<GameObject>(m_heads);
        partsByType[ePartType.Body] = new List<GameObject>(m_bodys);
        partsByType[ePartType.Weapon] = new List<GameObject>(m_weapons);

        // �� ���� Ÿ�Ը��� �⺻ ������ Ȱ��ȭ (�ִٸ�)
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
                Debug.LogWarning($"[{partType}] ���� ����� ����ֽ��ϴ�.");
            }
        }
    }



    private void Awake()
    {
        character = GetComponent<Character>();
        InitializeParts();
    }


    #region ����

    /// <summary>
    /// Ư�� ���� Ÿ���� ���� �� index�� �ش��ϴ� ������ �����մϴ�.
    /// </summary>
    /// <param name="partType">������ ���� Ÿ��</param>
    /// <param name="index">�ش� Ÿ�� ������ ������ �ε���</param>
    public void EquipPart(ePartType partType, int index)
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
    private void SetActiveExclusive(ePartType partType, int activeIndex)
    {
        List<GameObject> parts = partsByType[partType];

        for (int i = 0; i < parts.Count; i++)
        {
            bool shouldActivate = (i == activeIndex);
            
            parts[i].SetActive(shouldActivate);

            if (shouldActivate)
            {
                // ���� ������ ���� ������Ʈ
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
