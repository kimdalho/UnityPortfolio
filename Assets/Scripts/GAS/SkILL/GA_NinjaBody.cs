using System.Collections;
using UnityEngine;

public class GA_NinjaBody : GameAbility
{
    //ninjabody, // �� óġ �� 30�ʰ� ���� 30% ����
    public float condtionper = 0.3f;
    private float elapsedTime = 0f;

    private void Awake()
    {
        AbilityTag = eTagType.ninjabody;
    }
    private void Start()
    {
        owner.onKill += StartAbility;
    }
    private void StartAbility()
    {
        var _chance = Random.value; // 0.0 ~ 1.0
        if (_chance >= condtionper) return;

        // ���� ���� FX ����
        var _fx = FXFactory.Instance.GetFX(AbilityTag, owner.transform.position, Quaternion.identity);

        // �̹� ���� ���̶�� 
        if (owner.gameplayTagSystem.HasTag(AbilityTag))
        {
            elapsedTime = 0f;
            return;
        }

        owner.gameplayTagSystem.AddTag(AbilityTag);
        StartCoroutine(ExecuteAbility());
    }

    protected override IEnumerator ExecuteAbility()
    {
        elapsedTime += Time.deltaTime;

        // 30�� ���� ����
        while (elapsedTime < 30f)
        {
            var _ge = new GameEffectSelf();

            // int�� �̹Ƿ� �ݿø��Ͽ� ����
            _ge.effect.attackSpeed = Mathf.RoundToInt(owner.attribute.attackSpeed * 0.3f);
            _ge.ApplyGameplayEffectToSelf(owner);
            yield return null;
        }

        EndAbility();
    }
}