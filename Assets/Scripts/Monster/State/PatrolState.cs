using UnityEngine;

public class PatrolState : IState
{
    public bool IsState(Monster monster) => monster.patrolTargetPos != default(Vector3) && monster.chaseTarget == null;
    public void Enter(Monster monster)
    {
        monster.GetAnimator().SetBool("Move", true);
    }

    public void Action(Monster monster)
    {
        monster.PatrolAction();
    }

    public void Exit(Monster monster)
    {
        monster.GetAnimator().SetBool("Move", false);
    }
}
