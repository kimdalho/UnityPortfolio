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

    public bool IsCompleted { get; protected set; } = false;
    public async Task Execute()
    {
        IsCompleted = false;
        await Task.Delay(3000); // 예: 3초 대기
        IsCompleted = true;
    }

    public async Task AnimExecute(AnimState animState)
    {
        IsCompleted = false;
        Animator animator = owner.GetAnimator();
        animator.SetBool(animState.ToString(), true);
        AnimatorStateInfo animationState = animator.GetCurrentAnimatorStateInfo(0);
        float animationDuration = animationState.length;
        await Task.Delay(1000); // 예: 3초 대기
        IsCompleted = true;
    }
}
