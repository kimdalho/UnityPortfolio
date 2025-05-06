
using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public interface IOnKillEvent
{
   public void OnKill();
}


public class AttributeEntity : MonoBehaviour , IOnKillEvent
{
    public delegate void OnHitdelegate();
    
    public OnHitdelegate OnHit;

    public AttributeSet attribute;

    public Action Onkill;

    public void OnKill()
    {
        Onkill?.Invoke();
    }

    public void ApplyEffect(GameEffect effect)
    {
        //fxSystem.ExecuteFX(eTagType.Effect_NinjaSkill, owner.transform);
        foreach (var mod in effect.modifiers)
        {
            attribute.Modify(mod.attributeType, mod.value, effect.modifierOp);
        }

        if (effect.duration > 0f && 
            effect.eDurationPolicy == eDurationPolicy.HasDuration)
            StartCoroutine(RemoveEffectAfterDuration(effect));
    }
    public IEnumerator RemoveEffectAfterDuration(GameEffect effect)
    {
        yield return new WaitForSeconds(effect.duration);
        foreach (var mod in effect.modifiers)
        {
            attribute.RemoveModify(mod.attributeType, mod.value, effect.modifierOp);
        }

    }
}