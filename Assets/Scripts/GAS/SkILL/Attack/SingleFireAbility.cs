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
        var _armTrans = owner.transform.GetChild(0).GetChild(owner.transform.GetChild(0).childCount - 1);
        for (int i = 0; i < 8; i++) _armTrans = _armTrans.GetChild(0);

        float _fireDelay = 0f;

        for (int i = 0; i < FireCount; i++)
        {
            // �߻� Ƚ����ŭ �ִϸ��̼� ����
            //owner.GetAnimator().SetTrigger("Trg_Attack");
            owner.GetAnimator().Play("Attack", -1, 0f);
            _fireDelay = owner.GetAnimator().GetCurrentAnimatorStateInfo(0).length / _atkSpeed;

            //yield return new WaitForSeconds(delayAtkTime);

            InitProjectile(_armTrans, projectileType, _dir, true);

            yield return new WaitForSeconds(_fireDelay);
        }

        var _delay = Duration / _atkSpeed;

        yield return new WaitForSeconds(_delay);
        EndAbility();
    }
}
