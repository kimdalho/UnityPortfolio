using System.Collections;
using UnityEngine;

public class GA_NinjaBody : GameAbility
{
    //ninjabody, // �� óġ �� 30�ʰ� ���� 30% ����
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
            Debug.Log("�ɷ� ����");
            // ���� ���� FX ����
            owner.fxSystem?.ExecuteFX(AbilityTag);

            owner.gameplayTagSystem.AddTag(AbilityTag);

            // ������
            // int�� �̹Ƿ� �ݿø��Ͽ� ����
            var _increaseValue = Mathf.RoundToInt(owner.attribute.attackSpeed * 0.3f);

            var _ge = new GameEffectSelf();

            _ge.effect.attackSpeed = _increaseValue;
            _ge.ApplyGameplayEffectToSelf(owner);

            // 30�� ���� ����
            yield return new WaitForSeconds(Duration);

            _ge.effect.attackSpeed = -_increaseValue;
            _ge.ApplyGameplayEffectToSelf(owner);
        }

        EndAbility();
    }
}