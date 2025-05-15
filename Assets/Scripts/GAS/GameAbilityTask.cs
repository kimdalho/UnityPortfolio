using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.TextCore.Text;


/// <summary>
/// �ִϸ��̼�, ����Ʈ, ��� �� �߰� ���� ����
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
        await Task.Delay((int)(animationDuration * 1000)); // �� �������� �и��� ������ ��ȯ     
        owner.GetModelController().SetState(AnimState.Idle);
    }

    //���� GAS
    //character.GetAnimator().SetTrigger("Trg_Attack");
    //AnimatorStateInfo animationState = animator.GetCurrentAnimatorStateInfo(0);
    //float animationDuration = animationState.length;
    //if (animationDuration > Duration)
    //{
    //    Duration = animationDuration;
    //}
}
