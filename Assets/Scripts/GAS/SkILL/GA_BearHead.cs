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
        killCount = ++killCount % 4; // 4ų���� �ߵ�
        var _isTrig = killCount == 0; // 4ų���� �ߵ�

        if (_isTrig)
        {
            // ȸ�� FX ����
            var _fx = FXFactory.Instance.GetFX(AbilityTag, owner.transform.position, UnityEngine.Quaternion.identity);

            // 4ų���� ü�� 1ȸ��
            var _ge = new GameEffectSelf();
            _ge.effect.CurHart = 1;
            _ge.ApplyGameplayEffectToSelf(owner);
        }

        EndAbility();
        yield break;
    }
}