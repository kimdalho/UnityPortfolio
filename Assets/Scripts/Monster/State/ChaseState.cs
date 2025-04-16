using UnityEngine;

public class ChaseState : IState
{
    public bool IsState(Monster monster) => monster.chaseTarget != null &&
        Vector3.Distance(monster.transform.position, monster.chaseTarget.position) > monster.attackRange;
    public void Enter(Monster monster)
    {
        monster.GetAnimator().SetBool("Move", true);
    }
    public void Action(Monster monster)
    {
        monster.ChaseAction();
    }
    public void Exit(Monster monster)
    {
        monster.GetAnimator().SetBool("Move", false);
    }
}