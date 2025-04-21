using System.Collections;
using UnityEngine;

/// <summary>
/// 다수의 투사체를 발사하는 스킬
/// 다수의 투사체는 딜레이 없이 부채꼴 형태로 즉시 발사된다.
/// </summary>
public class MultipleFireAbility : FireAbility
{
    [SerializeField] private float fireAngleRange = 20f;

    protected override IEnumerator ExecuteAbility()
    {
        yield return base.ExecuteAbility();
        // 어빌리티 태그 적용
        yield return ApplyTag();

        //owner.GetAnimator().SetTrigger("Trg_Attack");
        owner.GetAnimator().Play("Attack", -1, 0f);

        // 발사 횟수가 1인 경우에는 정면으로 발사
        var _range = FireCount.Equals(1) ? 0f : fireAngleRange;

        var _startDir = Quaternion.AngleAxis(-_range, Vector3.up) * owner.transform.forward;
        var _endDir = Quaternion.AngleAxis(_range, Vector3.up) * owner.transform.forward;

        // Owner의 Hand Transform 탐색
        var _armTrans = owner.transform.GetChild(0).GetChild(owner.transform.GetChild(0).childCount - 1);
        for (int i = 0; i < 8; i++) _armTrans = _armTrans.GetChild(0);

        // Detect 혹은 Projectile이 생성되기까지 Delay되는 시간
        yield return new WaitForSeconds(delayAtkTime);

        FanShapeFire(_armTrans, projectileType, fireAngleRange, FireCount);

        //for (int i = 0; i < FireCount; i++)
        //{
        //    var _dir = Vector3.Lerp(_startDir, _endDir, (i + 1) / (float)FireCount);

        //    InitProjectile(_armTrans, projectileType, _dir, true);
        //    // 한번 에 발사하므로 한 프레임에 모두 생성되도록
        //    // yield return null;
        //}

        // Apply Delay
        var _animLength = owner.GetAnimator().GetCurrentAnimatorStateInfo(0).length;
        var _duration = _animLength > Duration ? _animLength : Duration;

        var _delay = _duration / Mathf.Max(owner.attribute.attackSpeed, 0.01f); // 0으로 나누는 것 방지

        yield return new WaitForSeconds(_delay);
        EndAbility();
    }
}
