using System.Collections;
using UnityEngine;

// 적 처치 시 30초간 공속 30% 증가
public class GA_NinjaBody : GameAbility
{
    public eTagType state = eTagType.NinjaBody_State_SpeedUp;

    protected override IEnumerator ExecuteAbility()
    {
        if (owner.GetGameplayTagSystem().HasTag(AbilityTag))
            yield break;

        StartAbility();
        owner.fxSystem.ExecuteFX(eTagType.Effect_NinjaSkill, owner.transform);
        yield return null;
    }

    private void StartAbility()
    {
        owner.GetGameplayTagSystem().AddTag(AbilityTag);        
        owner.Onkill += Onkill;
    }

    private void Onkill()
    {
        var baseAttackSpeed = owner.attribute.GetBaseValue(eAttributeType.AttackSpeed) * 0.3f;
        GameEffect effect = new GameEffect(eModifier.Multiply);
        effect.eDurationPolicy = eDurationPolicy.HasDuration;
        effect.duration = Duration;
        effect.AddModifier(eAttributeType.AttackSpeed, baseAttackSpeed);
        StartCoroutine(OnkillProcess());
    }

    private IEnumerator OnkillProcess()
    {
        if (owner.GetGameplayTagSystem().HasTag(state) == true)
            yield break;
        owner.GetGameplayTagSystem().AddTag(state);
        owner.fxSystem.ExecuteFX(eTagType.Effect_NinjaSkill, owner.transform);
        yield return new WaitForSeconds(Duration);
        owner.GetGameplayTagSystem().RemoveTag(state);
    }
    
    public override void EndAbility()
    {
        base.EndAbility();
        owner.Onkill -= Onkill;
    }
}