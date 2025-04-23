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
        var _increaseRatio = 0.2f; // 20% È®·ü
        var _addIncreaseRatio = 0.3f; // 30% È®·ü

        if (_chance <= _increaseRatio) // 20% È®·ü
        {
            eTagType _tag = AbilityTag;

            var _ge = new GameEffectSelf();
            _ge.effect.MaxHart = 1;
            _ge.ApplyGameplayEffectToSelf(owner);

            // ÃÖ´ë Ã¼·Â Ãß°¡ È¹µæ
            _chance = UnityEngine.Random.value; // 0.0 ~ 1.0
            if (owner.gameplayTagSystem.HasTag(eTagType.Roostersbody) && _chance <= _addIncreaseRatio)
            {
                _ge.ApplyGameplayEffectToSelf(owner);
                _tag = eTagType.Roostersbody;
            }

            // ÃÖ´ë Ã¼·Â Áõ°¡ FX ½ÇÇà
            var _fx = FXFactory.Instance.GetFX(_tag, owner.transform.position, UnityEngine.Quaternion.identity);

        }

        EndAbility();
        yield break;
    }
}