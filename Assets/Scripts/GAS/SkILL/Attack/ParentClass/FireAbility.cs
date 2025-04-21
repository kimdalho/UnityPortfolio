using System.Collections;
using UnityEngine;

/// <summary>
/// 투사체를 발사하는 스킬
/// </summary>
public abstract class FireAbility : AttackAbility
{
    [SerializeField] protected int baseFireCount = 1; // 기본 발사체 수
    [SerializeField] protected ProjectileType projectileType;
    [SerializeField] protected float delayAtkTime = 0.3f;

    protected int FireCount => increaseCount; // 증가된 발사체 수

    private int increaseCount = 0;

    public void ChangeProjectileType(ProjectileType newType) => projectileType = newType;

    protected override IEnumerator ExecuteAbility()
    {
        yield return base.ExecuteAbility();

        // Clown Hair를 가지고 있으면 투사체 3개로 변경
        increaseCount = owner.gameplayTagSystem.HasTag(eTagType.clownhair) ? GA_ClownHair. IncreaseCount : baseFireCount;

        // BoxBody를 가지고 있다면 기존 투사체 개수에서 2개 증가된 상태로 발사
        if (owner.gameplayTagSystem.HasTag(eTagType.boxbody))
        {
            increaseCount += 2;
            owner.gameplayTagSystem.RemoveTag(eTagType.boxbody);
        }

        yield break;
    }

    public override void EndAbility()
    {
        base.EndAbility();
        increaseCount = 0; // 발사체 증가수 리셋
    }
}
