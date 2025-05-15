using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.TextCore.Text;


/// <summary>
/// 애니메이션, 이펙트, 대기 등 추가 로직 관리
/// </summary>
public class GameAbilityTask
{
    private float duration;

    private Character owner;

    public GameAbilityTask(Character owner)
    {
        this.owner = owner;
    }
    
    public async Task AnimExecute(AnimState animState)
    {
        owner.GetModelController().SetState(animState);
        Animator animator = owner.GetAnimator();
        AnimatorStateInfo animationState = animator.GetCurrentAnimatorStateInfo(0);
        float animationDuration = animationState.length;
        await Task.Delay((int)(animationDuration * 1000)); // 초 단위에서 밀리초 단위로 변환     
        owner.GetModelController().SetState(AnimState.Idle);
    }

    //기존 GAS
    //character.GetAnimator().SetTrigger("Trg_Attack");
    //AnimatorStateInfo animationState = animator.GetCurrentAnimatorStateInfo(0);
    //float animationDuration = animationState.length;
    //if (animationDuration > Duration)
    //{
    //    Duration = animationDuration;
    //}
}
