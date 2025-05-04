using UnityEngine;

public class ReLoadState : IState
{
    private float duration = 2f; // 지속 시간
    private float elapsedTime = 0f;

    public bool IsState(Monster monster) => monster.CurBullet.Equals(0) && !monster.IsAtk;
    public void Enter(Monster monster)
    {
        monster.InitReLoad();
    }
    public void Action(Monster monster)
    {
        elapsedTime += Time.deltaTime;
        
        //IsReloading

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
