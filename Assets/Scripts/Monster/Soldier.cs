using UnityEngine;

public class Soldier : Monster
{
    [SerializeField] private Projectile BulletPrefab;
    [SerializeField] private Transform gunTrans;

    protected override void ReLoadAction()
    {
        if (!IsReLoad)
        {
            // �� ReLoad���� attackSpeed�� �ܰ������� �ö� -> �ִ� 3�� ������ ���Ŀ��� 1�� �ٽ� ���ư�
            attribute.attackSpeed = attribute.attackSpeed % 3 + 1;
        }
        base.ReLoadAction();
    }

    protected override void ExecuteAttack()
    {
        // �Ѿ� ����
        var _bullet = Instantiate(BulletPrefab, gunTrans.position, Quaternion.identity);
        _bullet.Initialized(this, transform.forward, LayerMask.GetMask("Player"));
    }
}
