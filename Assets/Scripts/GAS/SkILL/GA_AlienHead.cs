using System.Collections;

public class GA_AlienHead : GameAbility
{
    private void Awake()
    {
        AbilityTag = eTagType.alienhead;
    }

    private void Start()
    {
        owner.onKill += StartAbility;
    }

    private void StartAbility()
    {
        owner.gameplayTagSystem.AddTag(AbilityTag);
        StartCoroutine(ExecuteAbility());
    }

    protected override IEnumerator ExecuteAbility()
    {
        var _chance = UnityEngine.Random.value; // 0.0 ~ 1.0
        var _increaseRatio = 0.2f; // 20% Ȯ��
        var _addIncreaseRatio = 0.3f; // 30% Ȯ��

        if (_chance <= _increaseRatio) // 20% Ȯ��
        {
            eTagType _tag = AbilityTag;

            var _ge = new GameEffectSelf();
            _ge.effect.MaxHart = 1;
            _ge.ApplyGameplayEffectToSelf(owner);

            // �ִ� ü�� �߰� ȹ��
            _chance = UnityEngine.Random.value; // 0.0 ~ 1.0
            if (owner.gameplayTagSystem.HasTag(eTagType.Roostersbody) && _chance <= _addIncreaseRatio)
            {
                _ge.ApplyGameplayEffectToSelf(owner);
                _tag = eTagType.Roostersbody;
            }

            // �ִ� ü�� ���� FX ����
            var _fx = FXFactory.Instance.GetFX(_tag, owner.transform.position, UnityEngine.Quaternion.identity);

        }

        EndAbility();
        yield break;
    }
}