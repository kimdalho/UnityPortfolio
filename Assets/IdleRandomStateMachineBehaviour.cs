using UnityEngine;
/// <summary>
/// 3개의 타입의 Idle중 너무 자주 2,3 타입으로 변화가 되지않게끔(자주 변화하면 어색하고 산만해..)
/// 구성하는 로직
/// </summary>
public class IdleRandomStateMachineBehaviour : StateMachineBehaviour
{
    public int numberOfStates = 3;
    //최소값
    public float minNormTime = 2f;
    //최대얼마만큼 재생할것인지
    public float maxNormTime = 5f;
    //결정되는 애니메이션 지연값
    public float randomNormalTime;

    readonly int hashRandomIdle = Animator.StringToHash("RandomIdle");

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //상태(스테이트)에 들어올때 호출 즉 애니메이션 변화가 일어나는 시점
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //트렌지션에 필요한 시간
        randomNormalTime = Random.Range(minNormTime, maxNormTime);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //베이스 레이어 그리고 현제 상태의 이름과 다르다
        if (animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).fullPathHash
            == stateInfo.fullPathHash)
        {
            animator.SetInteger(hashRandomIdle, 1);
        }

        if(stateInfo.normalizedTime > randomNormalTime && !animator.IsInTransition(0))
        {
            animator.SetInteger(hashRandomIdle,Random.Range(1, numberOfStates));
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
