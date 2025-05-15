using UnityEngine;

public class PatrolState : IState
{
    public bool IsState(Monster monster) => !monster.onlyIdle && (monster.patrolTargetPos != default(Vector3) && monster.chaseTarget == null);
    public void Enter(Monster monster)
    {
        monster.GetModelController().SetState(AnimState.Move);
    }

    public void Action(Monster monster)
    {
        monster.PatrolAction();
    }

    public void Exit(Monster monster)
    {
        monster.GetModelController().SetState(AnimState.Idle);
    }
}
