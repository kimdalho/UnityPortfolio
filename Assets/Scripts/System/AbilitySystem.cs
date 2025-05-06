using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 능력 시스템을 관리하는 클래스입니다. 캐릭터의 능력을 등록, 발동,기능을 제공합니다.
/// 종료의 경우 스킬이 자신의 일을 처리하고 EndAbility를 반드시 호출해야합니다.
/// </summary>
public class AbilitySystem : MonoBehaviour
{
    // 태그별 능력을 저장하는 딕셔너리
    private Dictionary<eTagType, GameAbility> abilities = new Dictionary<eTagType, GameAbility>();

    void Start()
    {
        // 자식 오브젝트에서 모든 GameAbility를 찾아 등록합니다.
        GameAbility[] foundAbilities = GetComponentsInChildren<GameAbility>();
        foreach (var ability in foundAbilities)
        {
            abilities[ability.AbilityTag] = ability;
        }
    }

    /// <summary>
    /// 지정된 태그에 해당하는 능력을 활성화합니다.
    /// </summary>
    /// <param name="tag">능력 태그</param>
    /// <param name="owner">능력을 사용할 캐릭터</param>
    public void ActivateAbility(eTagType tag, Character owner)
    {
        if (abilities.TryGetValue(tag, out GameAbility ability))
        {
            ability.ActivateAbility(owner);
        }
    }

    /// <summary>
    /// 새로운 능력을 추가하고 즉시 발동합니다.
    /// </summary>
    /// <param name="newtag">새로운 능력의 태그</param>
    /// <param name="newAbility">새로운 능력 객체</param>
    /// <param name="owner">능력을 사용할 캐릭터</param>
    public void AddAndActivateAbility(eTagType newtag, GameAbility newAbility, Character owner)
    {
        if (abilities.TryAdd(newtag, newAbility))
        {
            // 새 능력을 자식 오브젝트로 추가하고 활성화합니다.
            newAbility.transform.SetParent(transform);
            newAbility.ActivateAbility(owner);
        }
    }

    /// <summary>
    /// 특정 태그에 해당하는 능력을 공격 능력으로 변경합니다.
    /// </summary>
    /// <param name="newtag">새로운 공격 능력의 태그</param>
    /// <param name="attackAbility">새로운 공격 능력 객체</param>
    public void AttackChange(eTagType newtag, GameAbility attackAbility)
    {
        if (newtag != eTagType.Attack)
        {
            Debug.LogError($"AbilitySystem -> Invalid method call {newtag.ToString()}");
            return;
        }
           
        // 기존 능력 종료 후 삭제
        abilities[newtag].EndAbility();
        Destroy(abilities[newtag].gameObject);

        // 새 공격 능력 등록
        abilities[newtag] = attackAbility;
    }

    /// <summary>
    /// 지정된 태그에 해당하는 능력을 반환합니다.
    /// </summary>
    /// <param name="findtag">찾고자 하는 능력의 태그</param>
    /// <returns>해당 태그의 능력</returns>
    public GameAbility GetGameAbility(eTagType findtag)
    {
        return abilities.ContainsKey(findtag) ? abilities[findtag] : null;
    }
}
