using UnityEngine;

public class Ninja : Samurai
{
    [SerializeField] private Transform armTrans;


    protected override void ExecuteAttack()
    {
        abilitySystem.ActivateAbility(eTagType.Attack, this);
    }
}