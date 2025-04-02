
using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

/// <summary>
/// 원래는 객체가 비활성화 되어도 사용가능한 테스크로 만들었으나
/// 게임에 사용되는 오브젝트이니 코루틴으로 사용되는게 맞다고 판단
/// 아쉽지만 이러면 발동시에 오브젝트는 반드시 활성화되어야함
/// </summary>
public abstract class GameAbility : MonoBehaviour
{
    public Character owner;
    public string AbilityTag;
    public string AbilityName;
    public float Cooldown;
    public float Duration;
    protected bool isOnCooldown = false;

    //스킬에 구독할 태그
    protected GameplayTagSystem tagSystem = new GameplayTagSystem();

    protected IEnumerator CoActivateAbility()
    {
        if (isOnCooldown || tagSystem.HasTag("Character.Stunned"))
        {
            Debug.Log($"{AbilityName} 사용 불가!");
            yield break;
        }

        Debug.Log($"{AbilityName} 발동!");
        isOnCooldown = true;

        yield return StartCoroutine(ExecuteAbility());  // 능력 실행
                                                        // 쿨다운 처리
        yield return new WaitForSeconds(Cooldown);
        
        isOnCooldown = false;
    }

    public void ActivateAbility(Character owner)
    {
        this.owner = owner;
        StartCoroutine(CoActivateAbility());
    }

    protected abstract IEnumerator ExecuteAbility();

    public void EndAbility()
    {
        
    }


}
