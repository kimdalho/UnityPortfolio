using UnityEngine;

public class DeadState : IState
{
    public bool IsState(Monster monster) => !monster.onlyIdle && monster.attribute.GetCurValue(eAttributeType.Health) <= 0;
    public void Enter(Monster monster)
    {
        monster.GetAnimator().SetTrigger("Trg_Dead");
    }
    public void Action(Monster monster)
    {
        monster.DeadAction();
    }
    public void Exit(Monster monster)
    {
    }
}
