using UnityEngine;

public class AttackState : IState
{
    public bool IsState(Monster monster) => monster.IsAtk || (monster.chaseTarget != null && !monster.IsAtkCool &&
        Vector3.Distance(monster.transform.position, monster.chaseTarget.position) <= monster.attackRange)
        && !monster.IsReloading;
    public void Enter(Monster monster)
    {
        monster.InitAttack();
    }
    public void Action(Monster monster)
    {
        monster.AttackAction();
    }
    public void Exit(Monster monster)
    {
        monster.animElapsed = 0f;
        monster.IsAtk = false;
    }
}
