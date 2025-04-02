using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem.XInput;
using static UnityEngine.UI.GridLayoutGroup;

public class AttackAbility : GameAbility
{
    protected override IEnumerator ExecuteAbility()
    {
        Animator animator = owner.GetComponent<Animator>();
        animator.SetTrigger("Trg_Attack");

        AnimatorStateInfo animationState = animator.GetCurrentAnimatorStateInfo(0);
        float animationDuration = animationState.length;
        if(animationDuration > Duration)
        {
            Duration = animationDuration;
        }

        yield return new WaitForSeconds(Duration);  // 지속 효과 처리
        EndAbility();
    }
}
