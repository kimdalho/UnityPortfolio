using System.Collections;
using UnityEngine;

public class GA_Attack_Bazooka : AttackAbility
{
    [SerializeField] private int fireCount = 1; // �߻� Ƚ��
    [SerializeField] private float fireAngleRange = 20f;
    [SerializeField] private ProjectileType projectileType;
    [SerializeField] private float delayAtkTime = 0.3f;



    protected override IEnumerator ExecuteAbility()
    {
        owner.GetAnimator().SetTrigger("Trg_Attack");

        // �߻� Ƚ���� 1�� ��쿡�� �������� �߻�
        var _range = fireCount.Equals(1) ? 0f : fireAngleRange;

        var _startDir = Quaternion.AngleAxis(-_range, Vector3.up) * owner.transform.forward;
        var _endDir = Quaternion.AngleAxis(_range, Vector3.up) * owner.transform.forward;

        // Owner�� Hand Transform Ž��
        var _armTrans = owner.transform.GetChild(0).GetChild(owner.transform.GetChild(0).childCount - 1);
        for (int i = 0; i < 8; i++) _armTrans = _armTrans.GetChild(0);

        // Detect Ȥ�� Projectile�� �����Ǳ���� Delay�Ǵ� �ð�
        yield return new WaitForSeconds(delayAtkTime);

        for (int i = 0; i < fireCount; i++)
        {
            var _dir = Vector3.Lerp(_startDir, _endDir, (i + 1) / (float)fireCount);

            var _projectile = ProjectileFactory.Instance.GetProjectile(projectileType, _armTrans.position, Quaternion.identity);
            _projectile.Initialized(owner, _dir.normalized, targetMask, AbilityTag, true);
        }

        // Apply Delay
        var _animLength = owner.GetAnimator().GetCurrentAnimatorStateInfo(0).length;
        var _duration = _animLength > Duration ? _animLength : Duration;

        var _delay = _duration / Mathf.Max(owner.attribute.attackSpeed, 0.01f); // 0���� ������ �� ����

        yield return new WaitForSeconds(_delay);
        EndAbility();
    }
}