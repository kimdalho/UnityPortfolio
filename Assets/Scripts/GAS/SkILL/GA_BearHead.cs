using System.Collections;
using UnityEngine;

//bearhead, //   적 4명 처치 시 체력 1 회복
public class GA_BearHead : GameAbility
{
    public int killcount;
    public readonly int CONDITION_Count = 4;
    public GameEffect effectHill;

    protected override IEnumerator ExecuteAbility()
    {
        killcount = 0;
        StartAbility();
        owner.fxSystem.ExecuteFX(eTagType.Effect_NinjaSkill, owner.transform);
        yield return null;
    }

    private void StartAbility()
    {
        owner.gameplayTagSystem.AddTag(AbilityTag);
        owner.Onkill += Onkill;
        if(effectHill == null)
        {
            effectHill = new GameEffect(eModifier.Add);
            effectHill.AddModifier(eAttributeType.Health, 1);
        }
    }

    private void Onkill()
    {
        StartCoroutine(OnkillProcess());
    }

    private IEnumerator OnkillProcess()
    {
        yield return null;  
        killcount++;   
        int remainder = killcount % CONDITION_Count;
        if(remainder <= 0)
        {
            owner.ApplyEffect(effectHill);
            owner.fxSystem.ExecuteFX(eTagType.Effect_NinjaSkill, owner.transform);
            killcount = 0;
        }        
    }
}