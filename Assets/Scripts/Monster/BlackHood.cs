using UnityEngine;

public class BlackHood : Monster
{
    protected override void ExecuteAttack()
    {
        abilitySystem.ActivateAbility("Attack", this);
    }
}