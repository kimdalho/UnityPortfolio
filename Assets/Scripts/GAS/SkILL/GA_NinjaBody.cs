using System.Collections;
using UnityEngine;

public class GA_NinjaBody : GameAbility
{
    //ninjabody, // 적 처치 시 30초간 공속 30% 증가
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

        // 공속 증가 FX 실행
        var _fx = FXFactory.Instance.GetFX(AbilityTag, owner.transform.position, Quaternion.identity);

        // 이미 실행 중이라면 
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

        // 30초 동안 지속
        while (elapsedTime < 30f)
        {
            var _ge = new GameEffectSelf();

            // int형 이므로 반올림하여 증가
            _ge.effect.attackSpeed = Mathf.RoundToInt(owner.attribute.attackSpeed * 0.3f);
            _ge.ApplyGameplayEffectToSelf(owner);
            yield return null;
        }

        EndAbility();
    }
}