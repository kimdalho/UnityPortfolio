using UnityEngine;

public class ReLoadState : IState
{
    private float duration = 2f; // ���� �ð�
    private float elapsedTime = 0f;

    public bool IsState(Monster monster) => ((monster.CurBullet <= 0) || (monster.IsReloading))
        && !monster.IsAtk;
    public void Enter(Monster monster)
    {
        monster.InitReLoad();
    }
    public void Action(Monster monster)
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= duration)
        {
            monster.ReLoadAction();
        }
    }
    public void Exit(Monster monster)
    {
        monster.animElapsed = 0f;
    }
}
