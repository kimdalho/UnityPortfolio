using UnityEngine;

public class HitState : IState
{
    public bool IsState(Monster monster) => monster.IsHit;
    public void Enter(Monster monster)
    {
        monster.GetAnimator().SetTrigger("Trg_Hit");
    }
    public void Action(Monster monster)
    {
        monster.HitAction();
    }
    public void Exit(Monster monster)
    {
        monster.animElapsed = 0f;
    }
}
