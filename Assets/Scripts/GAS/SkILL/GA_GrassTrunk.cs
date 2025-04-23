using System.Collections;
using UnityEngine;

public class GA_GrassTrunk : GameAbility
{
    private void Awake()
    {
        AbilityTag = eTagType.grasstrunk;
    }

    protected override IEnumerator ExecuteAbility()
    {
        // 한번만 업그레이드 or 지속적으로 현재 공격력을 tracking해서 값을 올려줌?
        owner.gameplayTagSystem.AddTag(AbilityTag);
        owner.attribute.atk = Mathf.RoundToInt(owner.attribute.atk * 1.5f);

        EndAbility();
        yield break;
    }
}