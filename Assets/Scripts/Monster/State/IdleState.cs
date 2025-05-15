using UnityEngine;

public class IdleState : IState
{
    public bool IsState(Monster monster) => true;
    public void Enter(Monster monster)
    {
        monster.GetModelController().SetState(AnimState.Idle);
    }
    public void Action(Monster monster)
    {
        monster.IdleAction();
    }
    public void Exit(Monster monster)
    {
    }
}
