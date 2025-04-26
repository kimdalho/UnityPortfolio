using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// 시전자의 공격력으로 대상에게 피해 서비스
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

        
        //맞은 대상이 죽은경우
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
