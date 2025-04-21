using UnityEngine;
using System.Collections;

/// <summary>
/// ����ü 3�� �߻�
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
        while (true/*Ư�� ���� �߻� �� �ش� �����Ƽ ����*/) yield return null;
        EndAbility();
    }

    public override void EndAbility()
    {
        base.EndAbility();
        if (!owner.gameplayTagSystem.HasTag(AbilityTag)) return;
        owner.gameplayTagSystem.RemoveTag(AbilityTag);
    }
}