using System.Collections;
using UnityEngine;

//피격 시 15% 확률로 1초간 무적
/// </summary>
public class GA_NinjaHead : GameAbility
{
    //성공 확률 0~1; 1이면 100퍼센트 성공
    public float condtionper = 0.15f;
    /// 발동 시속시간
    public float abilityDuration;

    //액티브되면 해당 태그가 플레이어에게 1초동안 추가됨
    private void Start()
    {
        Debug.Log(owner.gameObject.name);
        AbilityTag = eTagType.ninjahead;
        owner.onHit += StartAbility;
        owner.gameplayTagSystem.AddTag(AbilityTag);
    }

    private void StartAbility()
    {
        StartCoroutine(ExecuteAbility());
    }

    protected override IEnumerator ExecuteAbility()
    {

        float chance = Random.value; // 0.0 ~ 1.0
        Debug.Log($"찬스 {chance}");

        if (chance < condtionper) //0.3 이면 30퍼센트 성공
        {
            
        }
        else
        {
            yield break;
        }

        yield return new WaitForSeconds(abilityDuration);

        EndAbility();
    }

    public override void EndAbility()
    {
        owner.gameplayTagSystem.RemoveTag(AbilityTag);
    }


}

