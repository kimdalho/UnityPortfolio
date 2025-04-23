using System.Collections;
using UnityEngine;

public class GA_NinjaBody : GameAbility
{
    //ninjabody, // 적 처치 시 30초간 공속 30% 증가
    public float condtionper = 0.3f;

    private void Awake()
    {
        AbilityTag = eTagType.ninjabody;
        Duration = 5f;
    }

    private void Start()
    {
        owner.onKill += () => { ActivateAbility(owner); };
    }

    protected override IEnumerator ExecuteAbility()
    {
        var _chance = Random.value; // 0.0 ~ 1.0

        if (_chance <= condtionper)
        {
            Debug.Log("능력 실행");
            // 공속 증가 FX 실행
            owner.fxSystem?.ExecuteFX(AbilityTag);

            owner.gameplayTagSystem.AddTag(AbilityTag);

            // 증가값
            // int형 이므로 반올림하여 증가
            var _increaseValue = Mathf.RoundToInt(owner.attribute.attackSpeed * 0.3f);

            var _ge = new GameEffectSelf();

            _ge.effect.attackSpeed = _increaseValue;
            _ge.ApplyGameplayEffectToSelf(owner);

            // 30초 동안 유지
            yield return new WaitForSeconds(Duration);

            _ge.effect.attackSpeed = -_increaseValue;
            _ge.ApplyGameplayEffectToSelf(owner);
        }

        EndAbility();
    }
}