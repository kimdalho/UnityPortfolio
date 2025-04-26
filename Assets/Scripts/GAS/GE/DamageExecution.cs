using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// �������� ���ݷ����� ��󿡰� ���� ����
/// </summary>
public class DamageExecution : IGameEffectExecutionCalculation 
{
    public void Execute(Character source, AttributeEntity target)
    {

        Character targetCharacter = target as Character;
        if(targetCharacter != null)
        {
            bool onInvincible = targetCharacter.gameplayTagSystem.HasTag(eTagType.NinjaHead_State_Invincible);
            if(onInvincible)
            {
                return;
            }
        }
        
        float sourceATK = source.attribute.atk;
        target.attribute.CurHart -= sourceATK;
        target.attribute.CurHart = Mathf.Clamp(target.attribute.CurHart, 0, target.attribute.MaxHart);

        UnityEngine.Debug.Log($"Damage: {sourceATK} applied. Target HP: {target.attribute.CurHart}");

        
        //���� ����� �������
        if (target.attribute.CurHart <= 0)
        {
            ICanGameOver player = target.GetComponent<ICanGameOver>();
            if (player != null)
            {
                player.OnGameOver();
            }


            IOnKillEvent killer = source.GetComponent<IOnKillEvent>();
            if (killer != null)
            {
                killer.OnKill();
            }
        }

        target.OnHit?.Invoke();
    }


}
