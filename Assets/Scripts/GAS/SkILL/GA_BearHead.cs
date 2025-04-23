using System.Collections;

public class GA_BearHead : GameAbility
{
    private int killCount = 0;

    private void Awake()
    {
        AbilityTag = eTagType.bearhead;
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
        killCount = ++killCount % 4; // 4킬마다 발동
        var _isTrig = killCount == 0; // 4킬마다 발동

        if (_isTrig)
        {
            // 회복 FX 실행
            owner.fxSystem?.ExecuteFX(AbilityTag);

            // 4킬마다 체력 1회복
            var _ge = new GameEffectSelf();
            _ge.effect.CurHart = 1;
            _ge.ApplyGameplayEffectToSelf(owner);
        }

        EndAbility();
        yield break;
    }
}