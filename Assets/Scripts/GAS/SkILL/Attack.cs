using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class Attack : GameAbility
{
    //public string attackAnimation = "Attack"; // 공격 애니메이션 이름
    //public float rayDistance = 3f; // 레이캐스트 거리

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
    //    animator.Play(attackAnimation); // 애니메이션 실행
    //    yield return new WaitForSeconds(0.2f); // 몽타주 시작 후 타격 타이밍 대기

    //    PerformRaycast(); // 공격 판정
    //    yield return new WaitForSeconds(0.5f); // 공격 애니메이션 종료 대기

    //    EndAbility(); // 어빌리티 종료
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
