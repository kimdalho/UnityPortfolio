using UnityEngine;

public class ChaseState : IState
{
    public virtual bool IsState(Monster monster) => monster.chaseTarget != null &&
        Vector3.Distance(monster.transform.position, monster.chaseTarget.position) > monster.attackRange
         && !monster.IsReloading;
    public virtual void Enter(Monster monster)
    {
        monster.GetAnimator().SetBool("Move", true);
    }
    public virtual void Action(Monster monster)
    {
        monster.ChaseAction();
    }
    public virtual void Exit(Monster monster)
    {
        monster.GetAnimator().SetBool("Move", false);
    }
}