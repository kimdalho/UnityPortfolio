using System.Collections;

public class GA_RoostersHead : GameAbility
{
    private void Awake()
    {
        AbilityTag = eTagType.Roostershead;
    }

    protected override IEnumerator ExecuteAbility()
    {
        owner.gameplayTagSystem.AddTag(AbilityTag);
        while (true) yield return null;
        EndAbility();
    }
}