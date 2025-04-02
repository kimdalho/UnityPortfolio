
using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

/// <summary>
/// ������ ��ü�� ��Ȱ��ȭ �Ǿ ��밡���� �׽�ũ�� ���������
/// ���ӿ� ���Ǵ� ������Ʈ�̴� �ڷ�ƾ���� ���Ǵ°� �´ٰ� �Ǵ�
/// �ƽ����� �̷��� �ߵ��ÿ� ������Ʈ�� �ݵ�� Ȱ��ȭ�Ǿ����
/// </summary>
public abstract class GameAbility : MonoBehaviour
{
    public Character owner;
    public string AbilityTag;
    public string AbilityName;
    public float Cooldown;
    public float Duration;
    protected bool isOnCooldown = false;

    //��ų�� ������ �±�
    protected GameplayTagSystem tagSystem = new GameplayTagSystem();

    protected IEnumerator CoActivateAbility()
    {
        if (isOnCooldown || tagSystem.HasTag("Character.Stunned"))
        {
            Debug.Log($"{AbilityName} ��� �Ұ�!");
            yield break;
        }

        Debug.Log($"{AbilityName} �ߵ�!");
        isOnCooldown = true;

        yield return StartCoroutine(ExecuteAbility());  // �ɷ� ����
                                                        // ��ٿ� ó��
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
