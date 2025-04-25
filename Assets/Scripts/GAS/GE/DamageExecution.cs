using UnityEngine;


/// <summary>
/// 시전자의 공격력으로 대상에게 피해 서비스
/// </summary>
public class DamageExecution : IGameEffectExecutionCalculation 
{
    public void Execute(Character source, AttributeEntity target)
    {        
        float sourceATK = source.attribute.atk;
        target.attribute.CurHart -= sourceATK;
        UnityEngine.Debug.Log($"Damage: {sourceATK} applied. Target HP: {target.attribute.CurHart}");


        //맞은 대상이 죽은경우
        if(target.attribute.CurHart <= 0)
        {
            IOnKillEvent killer = source.GetComponent<IOnKillEvent>();
            if (killer != null)
            {
                killer.OnKill();
            }
        }


        target.onHit?.Invoke();



    }


}
