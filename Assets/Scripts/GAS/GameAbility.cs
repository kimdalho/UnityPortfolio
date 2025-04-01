using System;
using System.Threading.Tasks;
using UnityEngine;

public abstract class GameAbility : MonoBehaviour
{
    public string AbilityName;
    public float Cooldown;
    public float Duration;
    protected bool isOnCooldown = false;
    protected GameplayTagSystem tagSystem = new GameplayTagSystem();

    public async Task ActivateAbility()
    {
        if (isOnCooldown || tagSystem.HasTag("Character.Stunned"))
        {
            Debug.Log($"{AbilityName} 사용 불가!");
            return;
        }

        Debug.Log($"{AbilityName} 발동!");
        isOnCooldown = true;

        await ExecuteAbility();  // 능력 실행

        // 쿨다운 처리
        await Task.Delay(TimeSpan.FromSeconds(Cooldown));
        isOnCooldown = false;
    }

    protected abstract Task ExecuteAbility();
}
