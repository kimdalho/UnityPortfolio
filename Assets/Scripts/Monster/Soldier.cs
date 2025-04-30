using UnityEngine;

public class Soldier : Monster
{
    [SerializeField] private Projectile BulletPrefab;
    [SerializeField] private Transform gunTrans;

    public override void InitReLoad()
    {
        GameEffectSelf reloadEffect =  new GameEffectSelf();
        //reloadEffect

        attribute.attackSpeed = attribute.attackSpeed % 3 + 1;
        base.InitReLoad();
    }

    protected override void ExecuteAttack()
    {
        abilitySystem.ActivateAbility(eTagType.FanShapeFire, this);
    }
}
