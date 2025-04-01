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
            Debug.Log($"{AbilityName} ��� �Ұ�!");
            return;
        }

        Debug.Log($"{AbilityName} �ߵ�!");
        isOnCooldown = true;

        await ExecuteAbility();  // �ɷ� ����

        // ��ٿ� ó��
        await Task.Delay(TimeSpan.FromSeconds(Cooldown));
        isOnCooldown = false;
    }

    protected abstract Task ExecuteAbility();
}
