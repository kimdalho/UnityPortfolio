using System.Collections.Generic;
using UnityEngine;

public class AbilitySystem : MonoBehaviour
{
    private Dictionary<eTagType, GameAbility> abilities = new Dictionary<eTagType, GameAbility>();

    void Start()
    {
        // 모든 AbilityBase 자동 등록
        GameAbility[] foundAbilities = GetComponentsInChildren<GameAbility>();
        foreach (var ability in foundAbilities)
        {
            abilities[ability.AbilityTag] = ability;
        }
    }

    public void ActivateAbility(eTagType tag, Character owner)
    {
        if (abilities.TryGetValue(tag, out GameAbility ability))
        {
            ability.ActivateAbility(owner);
        }
    }

    public void DeactivateAbility(eTagType tag)
    {
        if (abilities.TryGetValue(tag, out GameAbility ability))
        {
            ability.EndAbility();
        }
    }

    /// <summary>
    /// 새로운 어빌리티를 획득하고 즉시 발동한다.
    /// </summary>
    /// <param name="newtag"></param>
    /// <param name="newAbility"></param>
    /// <param name="owner"></param>
    public void AddAndActivateAbility(eTagType newtag, GameAbility newAbility, Character owner)
    {
        if( abilities.TryAdd(newtag, newAbility))
        {
            newAbility.gameObject.transform.SetParent(transform);
            newAbility.ActivateAbility(owner);
        }        
    }

    public void AttackChange(eTagType newtag, GameAbility attackAbility)
    {
        abilities[newtag].EndAbility();
        Destroy(abilities[newtag].gameObject);
        abilities[newtag] = attackAbility;
    }

    public GameAbility GetGameAbility(eTagType findtag)
    {
        return abilities[findtag];
    }


}
