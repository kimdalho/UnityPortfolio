using System.Collections;
using UnityEngine;

/// <summary>
/// 게임 내 능력(스킬) 시스템의 기반 클래스입니다.
/// 객체가 비활성화되면 사용되지 않도록 코루틴 기반으로 구성했으며,
/// 능력 실행 시 오브젝트는 반드시 활성화되어 있어야 합니다.
/// </summary>
public abstract class GameAbility : MonoBehaviour
{
    [Header("Ability Info")]
    public string AbilityName;
    public eTagType AbilityTag;
    public float Duration;
    public LayerMask targetMask;

    [HideInInspector]
    public Character owner;

    private bool isOnCooldown = false;
    protected bool IsOnCooldown
    {
        get => isOnCooldown;
        set
        {
            isOnCooldown = value;

            // 몬스터일 경우 공격 쿨타임 동기화
            if (owner is Monster monster)
                monster.IsAtkCool = value;
        }
    }

    // 스킬 발동 제어용 태그 시스템 (예: 기절 상태 등)
    protected GameplayTagSystem tagSystem = new GameplayTagSystem();

    /// <summary>
    /// 능력 활성화 진입점. 오브젝트가 활성화되어 있어야 작동합니다.
    /// </summary>
    public virtual void ActivateAbility(Character owner)
    {
        this.owner = owner;
        StartCoroutine(CoActivateAbility());
    }

    /// <summary>
    /// 능력 실행 로직. 쿨타임 및 상태 체크 후 발동.
    /// </summary>
    protected IEnumerator CoActivateAbility()
    {
        var ownerTagSystem = owner.GetGameplayTagSystem();

        if (IsOnCooldown || ownerTagSystem.HasTag(eTagType.Stunned))
        {
            Debug.Log($"{owner.name} {AbilityName} 사용 불가!");
            yield break;
        }

        IsOnCooldown = true;

        try
        {
            yield return ExecuteAbility();
        }
        finally
        {            
            IsOnCooldown = false;
        }
    }

    /// <summary>
    /// 능력 효과 구현부 (상속 클래스에서 정의).
    /// </summary>
    protected abstract IEnumerator ExecuteAbility();

    /// <summary>
    /// 능력 강제 종료 시 호출됩니다.
    /// </summary>
    public virtual void EndAbility()
    {
        IsOnCooldown = false;
    }
}
