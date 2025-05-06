using System;
using UnityEngine;

public class Soldier : Monster
{
    [SerializeField] private Projectile BulletPrefab;
    [SerializeField] private Transform gunTrans;

    public override void InitReLoad()
    {
        BugCode();
        base.InitReLoad();
    }

    protected override void ExecuteAttack()
    {
        abilitySystem.ActivateAbility(eTagType.Attack, this);
    }

    //바꿔야함
    public void BugCode()
    {
        Debug.LogWarning("잘못된 코드");
        var attackSpeed = attribute.GetCurValue(eAttributeType.AttackSpeed);      
        attribute.SetCurrentValue(eAttributeType.AttackSpeed, attackSpeed % 3 + 1);
    }
}
