using UnityEngine;
/// <summary>
/// 3���� Ÿ���� Idle�� �ʹ� ���� 2,3 Ÿ������ ��ȭ�� �����ʰԲ�(���� ��ȭ�ϸ� ����ϰ� �길��..)
/// �����ϴ� ����
/// </summary>
public class IdleRandomStateMachineBehaviour : StateMachineBehaviour
{
    public int numberOfStates = 3;
    //�ּҰ�
    public float minNormTime = 2f;
    //�ִ�󸶸�ŭ ����Ұ�����
    public float maxNormTime = 5f;
    //�����Ǵ� �ִϸ��̼� ������
    public float randomNormalTime;

    readonly int hashRandomIdle = Animator.StringToHash("RandomIdle");

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //����(������Ʈ)�� ���ö� ȣ�� �� �ִϸ��̼� ��ȭ�� �Ͼ�� ����
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Ʈ�����ǿ� �ʿ��� �ð�
        randomNormalTime = Random.Range(minNormTime, maxNormTime);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //���̽� ���̾� �׸��� ���� ������ �̸��� �ٸ���
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
