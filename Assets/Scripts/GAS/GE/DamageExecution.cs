using UnityEngine;


/// <summary>
/// �������� ���ݷ����� ��󿡰� ���� ����
/// </summary>
public class DamageExecution : IGameEffectExecutionCalculation 
{
    public void Execute(Character source, AttributeEntity target)
    {        
        float sourceATK = source.attribute.atk;
        target.attribute.CurHart -= sourceATK;
        UnityEngine.Debug.Log($"Damage: {sourceATK} applied. Target HP: {target.attribute.CurHart}");


        //���� ����� �������
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
