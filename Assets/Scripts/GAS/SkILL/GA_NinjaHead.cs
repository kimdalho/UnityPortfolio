using System.Collections;
using UnityEngine;

//피격 시 30% 확률로 3초간 무적
/// </summary>
public class GA_NinjaHead : GameAbility
{
    //성공 확률 0~1; 1이면 100퍼센트 성공
    public float condtionper = 0.15f;

    public eTagType state = eTagType.NinjaHead_State_Invincible;

    protected override IEnumerator ExecuteAbility()
    {

        StartAbility();
        owner.fxSystem.ExecuteFX(eTagType.Effect_NinjaSkill,owner.transform);
        yield return null;
    }

    private void StartAbility()
    {
        owner.gameplayTagSystem.AddTag(AbilityTag);
        owner.OnHit += OnHit;
    }
    private void OnHit()
    {     
        StartCoroutine(OnHitProcess());
        owner.fxSystem.ExecuteFX(eTagType.Effect_NinjaSkill, owner.transform);
    }

    private IEnumerator OnHitProcess()
    {
        if (owner.gameplayTagSystem.HasTag(state) == true)
            yield break;
        var value = Random.value;
        if (value < condtionper)
        {
            owner.gameplayTagSystem.AddTag(state);
        }        
        yield return new WaitForSeconds(Duration);
        EndOnHitProcess();
    }


    private void EndOnHitProcess()
    {
        owner.gameplayTagSystem.RemoveTag(state);
    }

}

