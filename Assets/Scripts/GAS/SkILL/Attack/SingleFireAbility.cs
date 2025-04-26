using System.Collections;
using UnityEngine;

/// <summary>
/// 기본적으로 단일로 투사체를 발사하는 스킬
/// 투사체 증가 적용 시 일정 시간 간격으로 연속 발사가 된다.
/// </summary>
public class SingleFireAbility : FireAbility
{
    protected override IEnumerator ExecuteAbility()
    {
        yield return base.ExecuteAbility();
        yield return ApplyTag();

        var _atkSpeed = Mathf.Max(owner.attribute.attackSpeed, 0.01f); // 0으로 나누는 것 방지
        var _dir = owner.transform.forward;

        // Owner의 Hand Transform 탐색
        var _muzzleTrans = owner.transform.GetChild(0).GetChild(owner.transform.GetChild(0).childCount - 1);
        for (int i = 0; i < 8; i++) _muzzleTrans = _muzzleTrans.GetChild(0);

        float _fireDelay = 0f;

        for (int i = 0; i < FireCount; i++)
        {
            // 발사 횟수만큼 애니메이션 적용
            //owner.GetAnimator().SetTrigger("Trg_Attack");
            owner.GetAnimator().Play("Attack", -1, 0f);
            _fireDelay = owner.GetAnimator().GetCurrentAnimatorStateInfo(0).length / _atkSpeed;

            yield return new WaitForSeconds(0.1f);

            if (owner.curWeaponTrans != null)
            {
                _muzzleTrans = owner.curWeaponTrans.GetChild(0);
                FXFactory.Instance.GetFX("Shot", _muzzleTrans.position, Quaternion.LookRotation(_muzzleTrans.forward));
            }

            InitProjectile(_muzzleTrans, projectileType, _dir, true);

            yield return new WaitForSeconds(_fireDelay);
        }

        var _delay = Duration / _atkSpeed;

        yield return new WaitForSeconds(_delay);
        EndAbility();
    }
}
