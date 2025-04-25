using System.Collections;
using UnityEngine;

//�ǰ� �� 15% Ȯ���� 1�ʰ� ����
/// </summary>
public class GA_NinjaHead : GameAbility
{
    //���� Ȯ�� 0~1; 1�̸� 100�ۼ�Ʈ ����
    public float condtionper = 0.15f;
    /// �ߵ� �üӽð�
    public float abilityDuration;

    //��Ƽ��Ǹ� �ش� �±װ� �÷��̾�� 1�ʵ��� �߰���
    private void Start()
    {
        Debug.Log(owner.gameObject.name);
        AbilityTag = eTagType.ninjahead;
        owner.onHit += StartAbility;
        owner.gameplayTagSystem.AddTag(AbilityTag);
    }

    private void StartAbility()
    {
        StartCoroutine(ExecuteAbility());
    }

    protected override IEnumerator ExecuteAbility()
    {

        float chance = Random.value; // 0.0 ~ 1.0
        Debug.Log($"���� {chance}");

        if (chance < condtionper) //0.3 �̸� 30�ۼ�Ʈ ����
        {
            
        }
        else
        {
            yield break;
        }

        yield return new WaitForSeconds(abilityDuration);

        EndAbility();
    }

    public override void EndAbility()
    {
        owner.gameplayTagSystem.RemoveTag(AbilityTag);
    }


}

