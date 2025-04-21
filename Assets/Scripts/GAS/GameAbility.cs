
using System.Collections;
using UnityEngine;
/// <summary>
/// <summary>
/// ������ ��ü�� ��Ȱ��ȭ �Ǿ ��밡���� �׽�ũ�� ���������
/// ���ӿ� ���Ǵ� ������Ʈ�̴� �ڷ�ƾ���� ���Ǵ°� �´ٰ� �Ǵ�
/// �ƽ����� �̷��� �ߵ��ÿ� ������Ʈ�� �ݵ�� Ȱ��ȭ�Ǿ����
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

    //��ų�� ������ �±�
    protected GameplayTagSystem tagSystem = new GameplayTagSystem();

    protected IEnumerator CoActivateAbility()
    {
        if (IsOnCooldown || tagSystem.HasTag(eTagType.Stunned))
        {
            Debug.Log($"{AbilityName} ��� �Ұ�!");
            yield break;
        }

        Debug.Log($"{AbilityName} �ߵ�!");
        IsOnCooldown = true;

        // StartCoroutine���� ȣ�� �� �������� �������� �����
        //yield return StartCoroutine(ExecuteAbility());
        yield return ExecuteAbility();  // �ɷ� ����

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
