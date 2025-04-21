using UnityEngine;


/// <summary>
/// �������� ���ݷ����� ��󿡰� ���� ����
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
