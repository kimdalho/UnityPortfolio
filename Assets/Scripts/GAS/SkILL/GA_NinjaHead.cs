using System.Collections;
using UnityEngine;

//�ǰ� �� 30% Ȯ���� 3�ʰ� ����
/// </summary>
public class GA_NinjaHead : GameAbility
{
    //���� Ȯ�� 0~1; 1�̸� 100�ۼ�Ʈ ����
    public float condtionper = 0.15f;

    public eTagType state = eTagType.NinjaHead_State_Invincible;

    protected override IEnumerator ExecuteAbility()
    {

        StartAbility();
        owner.fxSystem.ExecuteFX(eTagType.Effect_NinjaSkill,owner.transform);
        yield return null;
    }

    private void StartAbility()
    {
        owner.gameplayTagSystem.AddTag(AbilityTag);
        owner.OnHit += OnHit;
    }
    private void OnHit()
    {     
        StartCoroutine(OnHitProcess());
        owner.fxSystem.ExecuteFX(eTagType.Effect_NinjaSkill, owner.transform);
    }

    private IEnumerator OnHitProcess()
    {
        if (owner.gameplayTagSystem.HasTag(state) == true)
            yield break;
        var value = Random.value;
        if (value < condtionper)
        {
            owner.gameplayTagSystem.AddTag(state);
        }        
        yield return new WaitForSeconds(Duration);
        EndOnHitProcess();
    }


    private void EndOnHitProcess()
    {
        owner.gameplayTagSystem.RemoveTag(state);
    }

}

