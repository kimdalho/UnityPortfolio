using UnityEngine;
using System.Collections;

/// <summary>
/// 투사체 3개 발사
/// </summary>
public class GA_ClownHair : GameAbility
{
    public static readonly int IncreaseCount = 3;

    private void Awake()
    {
        AbilityTag = eTagType.clownhair;
    }

    protected override IEnumerator ExecuteAbility()
    {
        owner.gameplayTagSystem.AddTag(AbilityTag);
        while (true/*특정 조건 발생 시 해당 어빌리티 삭제*/) yield return null;
        EndAbility();
    }

    public override void EndAbility()
    {
        base.EndAbility();
        if (!owner.gameplayTagSystem.HasTag(AbilityTag)) return;
        owner.gameplayTagSystem.RemoveTag(AbilityTag);
    }
}