using UnityEngine;

public class Burner : Monster
{
    protected override void ExecuteAttack()
    {
        abilitySystem.ActivateAbility(eTagType.FlameThrower, this);
    }

    public override void AttackAction()
    {
        
    }
}
