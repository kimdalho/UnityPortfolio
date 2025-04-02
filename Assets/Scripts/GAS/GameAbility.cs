
using System.Collections;
using UnityEngine;

/// <summary>
/// ������ ��ü�� ��Ȱ��ȭ �Ǿ ��밡���� �׽�ũ�� ���������
/// ���ӿ� ���Ǵ� ������Ʈ�̴� �ڷ�ƾ���� ���Ǵ°� �´ٰ� �Ǵ�
/// �ƽ����� �̷��� �ߵ��ÿ� ������Ʈ�� �ݵ�� Ȱ��ȭ�Ǿ����
/// </summary>
public abstract class GameAbility : MonoBehaviour
{
    public Character owner;
    public string AbilityName;
    public float Cooldown;
    public float Duration;
    protected bool isOnCooldown = false;
    protected GameplayTagSystem tagSystem = new GameplayTagSystem();

    public string attackAnimation;

    public Coroutine Handle;

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

    public Coroutine ActivateAbility()
    {
      Handle = StartCoroutine(CoActivateAbility());
      return Handle;
    }

    protected abstract IEnumerator ExecuteAbility();

    public void EndAbility()
    {       
        Handle = null;
        Destroy(this.gameObject);
    }


}
