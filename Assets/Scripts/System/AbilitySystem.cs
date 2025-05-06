using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ɷ� �ý����� �����ϴ� Ŭ�����Դϴ�. ĳ������ �ɷ��� ���, �ߵ�,����� �����մϴ�.
/// ������ ��� ��ų�� �ڽ��� ���� ó���ϰ� EndAbility�� �ݵ�� ȣ���ؾ��մϴ�.
/// </summary>
public class AbilitySystem : MonoBehaviour
{
    // �±׺� �ɷ��� �����ϴ� ��ųʸ�
    private Dictionary<eTagType, GameAbility> abilities = new Dictionary<eTagType, GameAbility>();

    void Start()
    {
        // �ڽ� ������Ʈ���� ��� GameAbility�� ã�� ����մϴ�.
        GameAbility[] foundAbilities = GetComponentsInChildren<GameAbility>();
        foreach (var ability in foundAbilities)
        {
            abilities[ability.AbilityTag] = ability;
        }
    }

    /// <summary>
    /// ������ �±׿� �ش��ϴ� �ɷ��� Ȱ��ȭ�մϴ�.
    /// </summary>
    /// <param name="tag">�ɷ� �±�</param>
    /// <param name="owner">�ɷ��� ����� ĳ����</param>
    public void ActivateAbility(eTagType tag, Character owner)
    {
        if (abilities.TryGetValue(tag, out GameAbility ability))
        {
            ability.ActivateAbility(owner);
        }
    }

    /// <summary>
    /// ���ο� �ɷ��� �߰��ϰ� ��� �ߵ��մϴ�.
    /// </summary>
    /// <param name="newtag">���ο� �ɷ��� �±�</param>
    /// <param name="newAbility">���ο� �ɷ� ��ü</param>
    /// <param name="owner">�ɷ��� ����� ĳ����</param>
    public void AddAndActivateAbility(eTagType newtag, GameAbility newAbility, Character owner)
    {
        if (abilities.TryAdd(newtag, newAbility))
        {
            // �� �ɷ��� �ڽ� ������Ʈ�� �߰��ϰ� Ȱ��ȭ�մϴ�.
            newAbility.transform.SetParent(transform);
            newAbility.ActivateAbility(owner);
        }
    }

    /// <summary>
    /// Ư�� �±׿� �ش��ϴ� �ɷ��� ���� �ɷ����� �����մϴ�.
    /// </summary>
    /// <param name="newtag">���ο� ���� �ɷ��� �±�</param>
    /// <param name="attackAbility">���ο� ���� �ɷ� ��ü</param>
    public void AttackChange(eTagType newtag, GameAbility attackAbility)
    {
        if (newtag != eTagType.Attack)
        {
            Debug.LogError($"AbilitySystem -> Invalid method call {newtag.ToString()}");
            return;
        }
           
        // ���� �ɷ� ���� �� ����
        abilities[newtag].EndAbility();
        Destroy(abilities[newtag].gameObject);

        // �� ���� �ɷ� ���
        abilities[newtag] = attackAbility;
    }

    /// <summary>
    /// ������ �±׿� �ش��ϴ� �ɷ��� ��ȯ�մϴ�.
    /// </summary>
    /// <param name="findtag">ã���� �ϴ� �ɷ��� �±�</param>
    /// <returns>�ش� �±��� �ɷ�</returns>
    public GameAbility GetGameAbility(eTagType findtag)
    {
        return abilities.ContainsKey(findtag) ? abilities[findtag] : null;
    }
}
