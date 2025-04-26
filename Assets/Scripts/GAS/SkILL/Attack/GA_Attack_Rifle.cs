using System.Collections;

public class GA_Attack_Rifle : AttackAbility
{
    private void Awake()
    {
       Character ownerChar =  owner.GetComponent<Character>();
       if (ownerChar != null)
       {
           ownerChar.GetAbilitySystem().AttackChange(this);
       }
    }

    protected override IEnumerator ExecuteAbility()
    {
       yield return base.ExecuteAbility();
    }
}