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

    public void Add(eTagType newtag, GameAbility newAbility)
    {
        if( abilities.TryAdd(newtag, newAbility))
        {
            newAbility.gameObject.transform.SetParent(transform);
        }        
    }

    
}
