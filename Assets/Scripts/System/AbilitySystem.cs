using System.Collections.Generic;
using UnityEngine;

public class AbilitySystem : MonoBehaviour
{
    private Dictionary<string, GameAbility> abilities = new Dictionary<string, GameAbility>();

    void Start()
    {
        // 모든 AbilityBase 자동 등록
        GameAbility[] foundAbilities = GetComponentsInChildren<GameAbility>();
        foreach (var ability in foundAbilities)
        {
            abilities[ability.AbilityTag] = ability;
        }
    }

    public void ActivateAbility(string tag, Character owner)
    {
        if (abilities.TryGetValue(tag, out GameAbility ability))
        {
            ability.ActivateAbility(owner);
        }
    }

    public void DeactivateAbility(string tag)
    {
        if (abilities.TryGetValue(tag, out GameAbility ability))
        {
            ability.EndAbility();
        }
    }
}
