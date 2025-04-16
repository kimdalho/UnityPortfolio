using UnityEngine;

public class ReLoadState : IState
{
    public bool IsState(Monster monster) => monster.CurBullet.Equals(0) && !monster.IsAtk;
    public void Enter(Monster monster)
    {
        monster.InitReLoad();
    }
    public void Action(Monster monster)
    {
        monster.ReLoadAction();
    }
    public void Exit(Monster monster)
    {
        monster.animElapsed = 0f;
    }
}
