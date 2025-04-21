using System.Collections;
using UnityEngine;

/// <summary>
/// �ټ��� ����ü�� �߻��ϴ� ��ų
/// �ټ��� ����ü�� ������ ���� ��ä�� ���·� ��� �߻�ȴ�.
/// </summary>
public class MultipleFireAbility : FireAbility
{
    [SerializeField] private float fireAngleRange = 20f;

    protected override IEnumerator ExecuteAbility()
    {
        yield return base.ExecuteAbility();
        // �����Ƽ �±� ����
        yield return ApplyTag();

        //owner.GetAnimator().SetTrigger("Trg_Attack");
        owner.GetAnimator().Play("Attack", -1, 0f);

        // �߻� Ƚ���� 1�� ��쿡�� �������� �߻�
        var _range = FireCount.Equals(1) ? 0f : fireAngleRange;

        var _startDir = Quaternion.AngleAxis(-_range, Vector3.up) * owner.transform.forward;
        var _endDir = Quaternion.AngleAxis(_range, Vector3.up) * owner.transform.forward;

        // Owner�� Hand Transform Ž��
        var _armTrans = owner.transform.GetChild(0).GetChild(owner.transform.GetChild(0).childCount - 1);
        for (int i = 0; i < 8; i++) _armTrans = _armTrans.GetChild(0);

        // Detect Ȥ�� Projectile�� �����Ǳ���� Delay�Ǵ� �ð�
        yield return new WaitForSeconds(delayAtkTime);

        FanShapeFire(_armTrans, projectileType, fireAngleRange, FireCount);

        //for (int i = 0; i < FireCount; i++)
        //{
        //    var _dir = Vector3.Lerp(_startDir, _endDir, (i + 1) / (float)FireCount);

        //    InitProjectile(_armTrans, projectileType, _dir, true);
        //    // �ѹ� �� �߻��ϹǷ� �� �����ӿ� ��� �����ǵ���
        //    // yield return null;
        //}

        // Apply Delay
        var _animLength = owner.GetAnimator().GetCurrentAnimatorStateInfo(0).length;
        var _duration = _animLength > Duration ? _animLength : Duration;

        var _delay = _duration / Mathf.Max(owner.attribute.attackSpeed, 0.01f); // 0���� ������ �� ����

        yield return new WaitForSeconds(_delay);
        EndAbility();
    }
}
