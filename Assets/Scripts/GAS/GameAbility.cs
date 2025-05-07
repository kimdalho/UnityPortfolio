using System.Collections;
using UnityEngine;

/// <summary>
/// ���� �� �ɷ�(��ų) �ý����� ��� Ŭ�����Դϴ�.
/// ��ü�� ��Ȱ��ȭ�Ǹ� ������ �ʵ��� �ڷ�ƾ ������� ����������,
/// �ɷ� ���� �� ������Ʈ�� �ݵ�� Ȱ��ȭ�Ǿ� �־�� �մϴ�.
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

            // ������ ��� ���� ��Ÿ�� ����ȭ
            if (owner is Monster monster)
                monster.IsAtkCool = value;
        }
    }

    // ��ų �ߵ� ����� �±� �ý��� (��: ���� ���� ��)
    protected GameplayTagSystem tagSystem = new GameplayTagSystem();

    /// <summary>
    /// �ɷ� Ȱ��ȭ ������. ������Ʈ�� Ȱ��ȭ�Ǿ� �־�� �۵��մϴ�.
    /// </summary>
    public virtual void ActivateAbility(Character owner)
    {
        this.owner = owner;
        StartCoroutine(CoActivateAbility());
    }

    /// <summary>
    /// �ɷ� ���� ����. ��Ÿ�� �� ���� üũ �� �ߵ�.
    /// </summary>
    protected IEnumerator CoActivateAbility()
    {
        var ownerTagSystem = owner.GetGameplayTagSystem();

        if (IsOnCooldown || ownerTagSystem.HasTag(eTagType.Stunned))
        {
            Debug.Log($"{owner.name} {AbilityName} ��� �Ұ�!");
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
    /// �ɷ� ȿ�� ������ (��� Ŭ�������� ����).
    /// </summary>
    protected abstract IEnumerator ExecuteAbility();

    /// <summary>
    /// �ɷ� ���� ���� �� ȣ��˴ϴ�.
    /// </summary>
    public virtual void EndAbility()
    {
        IsOnCooldown = false;
    }
}
