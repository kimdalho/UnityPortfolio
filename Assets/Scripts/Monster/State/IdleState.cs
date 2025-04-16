using UnityEngine;

public class IdleState : IState
{
    public bool IsState(Monster monster) => true;
    public void Enter(Monster monster)
    {
    }
    public void Action(Monster monster)
    {
        monster.IdleAction();
    }
    public void Exit(Monster monster)
    {
    }
}
