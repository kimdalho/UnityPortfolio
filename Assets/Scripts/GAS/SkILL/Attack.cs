using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class Attack : GameAbility
{
    //public string attackAnimation = "Attack"; // ���� �ִϸ��̼� �̸�
    //public float rayDistance = 3f; // ����ĳ��Ʈ �Ÿ�

    //private Coroutine abilityCoroutine;

    //public override void ActivateAbility()
    //{
    //    if (abilityCoroutine == null)
    //    {
    //        abilityCoroutine = StartCoroutine(AttackSequence());
    //    }
    //}

    //public override void Deactivate()
    //{
    //    if (abilityCoroutine != null)
    //    {
    //        StopCoroutine(abilityCoroutine);
    //        abilityCoroutine = null;
    //    }
    //}

    //private IEnumerator AttackSequence()
    //{
    //    Animator animator = owner.GetComponent<Animator>();
    //    animator.Play(attackAnimation); // �ִϸ��̼� ����
    //    yield return new WaitForSeconds(0.2f); // ��Ÿ�� ���� �� Ÿ�� Ÿ�̹� ���

    //    PerformRaycast(); // ���� ����
    //    yield return new WaitForSeconds(0.5f); // ���� �ִϸ��̼� ���� ���

    //    EndAbility(); // �����Ƽ ����
    //}


    //protected override Task ExecuteAbility()
    //{
    //    throw new System.NotImplementedException();
    //}
    protected override IEnumerator ExecuteAbility()
    {
        throw new System.NotImplementedException();
    }
}
