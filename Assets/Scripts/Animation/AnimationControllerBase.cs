using UnityEngine;
using UnityEngine.Playables;

public abstract class AnimationControllerBase :  MonoBehaviour
{
    [SerializeField] public Animator animator;
    protected AnimState currentState;
    protected CharacterController characterController;
    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void SetMoveDirection(float x,float y)
    {
        if (x == 0 && y == 0)
        {
            animator.SetBool(GlobalAnim.IsMove, false);
            return;
        }


        animator.SetBool(GlobalAnim.IsMove, true);
        animator.SetFloat(GlobalAnim.MoveX, x);
        animator.SetFloat(GlobalAnim.MoveY, y);
    }

    public void PlayMove()
    {
        animator.SetBool(GlobalAnim.IsMove, true);
    }



    // 외부에서 점프 호출
    public void PlayJump()
    {
        animator.SetBool(GlobalAnim.IsJumping, true);
    }

    // 애니메이션 이벤트에서 이 함수 호출 (점프 애니 끝날 때)
    public void OnJumpFinished()
    {
        animator.SetBool(GlobalAnim.IsJumping, false);
    }

    // 외부에서 공격 호출
    public void PlayAttack()
    {
        animator.SetBool(GlobalAnim.IsAttacking, true);
    }

    public void OnAttackFinished()
    {
        animator.SetBool(GlobalAnim.IsAttacking, false);
    }

    // 외부에서 재장전 호출
    public void PlayReload()
    {
        animator.SetBool(GlobalAnim.IsReloading, true);
    }

    public void OnReloadFinished()
    {
        animator.SetBool(GlobalAnim.IsReloading, false);
    }

    public void PlayDeath()
    {
        animator.SetBool(GlobalAnim.IsDead, true);
    }

    public void SetState(AnimState newState)
    {
        currentState = newState;
        animator.SetBool(GlobalAnim.IsJumping, newState == AnimState.JumpStart);
        animator.SetBool(GlobalAnim.IsFalling, newState == AnimState.InAir);
        animator.SetBool(GlobalAnim.IsLanding, newState == AnimState.Land);
        animator.SetBool(GlobalAnim.IsAttacking, newState == AnimState.Attack);
        animator.SetBool(GlobalAnim.IsReloading, newState == AnimState.Reload);
        animator.SetBool(GlobalAnim.IsDead, newState == AnimState.Death);
    }

    public AnimState GetState()
    {
        return currentState;
    }
}