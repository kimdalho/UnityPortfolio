using UnityEngine;

public class Soldier : Monster
{
    [SerializeField] private Projectile BulletPrefab;
    [SerializeField] private Transform gunTrans;

    protected override void ReLoadAction()
    {
        if (!IsReLoad)
        {
            // 매 ReLoad마다 attackSpeed가 단계적으로 올라감 -> 최대 3에 도달한 이후에는 1로 다시 돌아감
            attribute.attackSpeed = attribute.attackSpeed % 3 + 1;
        }
        base.ReLoadAction();
    }

    protected override void ExecuteAttack()
    {
        // 총알 생성
        var _bullet = Instantiate(BulletPrefab, gunTrans.position, Quaternion.identity);
        _bullet.Initialized(this, transform.forward, LayerMask.GetMask("Player"));
    }
}
