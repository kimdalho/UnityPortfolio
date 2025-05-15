using UnityEngine;

public class ChaseState : IState
{

    //        Vector3.Distance(monster.transform.position, monster.chaseTarget.position) > monster.attackRange
    public virtual bool IsState(Monster monster) => !monster.onlyIdle && (monster.aggro || (monster.chaseTarget != null 
         && !monster.IsReloading));
    public virtual void Enter(Monster monster)
    {
        monster.GetModelController().SetState(AnimState.Move);
    }
    public virtual void Action(Monster monster)
    {
        monster.ChaseAction();
    }
    public virtual void Exit(Monster monster)
    {
        monster.GetModelController().SetState(AnimState.Idle);
    }
}