
using System.Collections;
using UnityEngine;
/// <summary>
/// <summary>
/// 원래는 객체가 비활성화 되어도 사용가능한 테스크로 만들었으나
/// 게임에 사용되는 오브젝트이니 코루틴으로 사용되는게 맞다고 판단
/// 아쉽지만 이러면 발동시에 오브젝트는 반드시 활성화되어야함
/// </summary>
public abstract class GameAbility : MonoBehaviour
{
    public Character owner;
    public eTagType AbilityTag;
    public string AbilityName;    
    public float Duration;
    public LayerMask targetMask;

    protected bool isOnCooldown = false;
    protected virtual bool IsOnCooldown
    {
        get => isOnCooldown;
        set => isOnCooldown = value;
    }

    //스킬에 구독할 태그
    protected GameplayTagSystem tagSystem = new GameplayTagSystem();

    protected IEnumerator CoActivateAbility()
    {
        if (IsOnCooldown || tagSystem.HasTag(eTagType.Stunned))
        {
            Debug.Log($"{AbilityName} 사용 불가!");
            yield break;
        }

        Debug.Log($"{AbilityName} 발동!");
        IsOnCooldown = true;

        // StartCoroutine으로 호출 시 독립적인 로직으로 실행됨
        //yield return StartCoroutine(ExecuteAbility());
        yield return ExecuteAbility();  // 능력 실행

        IsOnCooldown = false;
    }

    public void ActivateAbility(Character owner)
    {
        this.owner = owner;
        StartCoroutine(CoActivateAbility());
    }

    protected abstract IEnumerator ExecuteAbility();

    public virtual void EndAbility()
    {
        if (!owner.gameplayTagSystem.HasTag(AbilityTag)) return;
        owner.gameplayTagSystem.RemoveTag(AbilityTag);
    }


}
