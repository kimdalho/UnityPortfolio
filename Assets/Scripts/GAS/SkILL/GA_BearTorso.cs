using System.Collections;

public class GA_BearTorso : GameAbility
{
    private void Awake()
    {
        AbilityTag = eTagType.beartorso;
    }

    public static bool ReturnSuccessResult()
    {
        var _odds = 1f;
        return UnityEngine.Random.Range(0f, 1f) < _odds;
    }

    protected override IEnumerator ExecuteAbility()
    {
        owner.gameplayTagSystem.AddTag(AbilityTag);

        while (true) yield return null;

        EndAbility();
    }
}