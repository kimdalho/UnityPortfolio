using UnityEngine;

public class Ninja : Samurai
{
    [SerializeField] private int throwCount = 5;
    [SerializeField] private Transform armTrans;

    protected override void Initialized()
    {
        base.Initialized();
        throwCount = Random.Range(2, 5);
    }

    protected override void ExecuteAttack()
    {
        abilitySystem.ActivateAbility("FanShapeFire", this);
    }
}