using UnityEngine;


/// <summary>
/// 시전자의 공격력으로 대상에게 피해 서비스
/// </summary>
public class DamageExecution : IGameEffectExecutionCalculation 
{
    public void Execute(Character source, AttributeEntity target)
    {        
        int sourceATK = source.attribute.atk;
        target.attribute.CurHart -= sourceATK;
        UnityEngine.Debug.Log($"Damage: {sourceATK} applied. Target HP: {target.attribute.CurHart}");        

        target.onHit?.Invoke();
    }
}
