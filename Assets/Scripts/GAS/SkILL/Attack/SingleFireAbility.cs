using System.Collections;
using UnityEngine;

/// <summary>
/// �⺻������ ���Ϸ� ����ü�� �߻��ϴ� ��ų
/// ����ü ���� ���� �� ���� �ð� �������� ���� �߻簡 �ȴ�.
/// </summary>
public class SingleFireAbility : FireAbility
{
    protected override IEnumerator ExecuteAbility()
    {
        yield return base.ExecuteAbility();
        yield return ApplyTag();

        var _atkSpeed = Mathf.Max(owner.attribute.attackSpeed, 0.01f); // 0���� ������ �� ����
        var _dir = owner.transform.forward;

        // Owner�� Hand Transform Ž��
        var _muzzleTrans = owner.transform.GetChild(0).GetChild(owner.transform.GetChild(0).childCount - 1);
        for (int i = 0; i < 8; i++) _muzzleTrans = _muzzleTrans.GetChild(0);

        float _fireDelay = 0f;

        for (int i = 0; i < FireCount; i++)
        {
            // �߻� Ƚ����ŭ �ִϸ��̼� ����
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
