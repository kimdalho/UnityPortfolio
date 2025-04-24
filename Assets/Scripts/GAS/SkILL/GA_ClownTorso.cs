using System.Collections;

public class GA_ClownTorso : GameAbility
{
    private void Awake()
    {
        AbilityTag = eTagType.clowntorso;   
    }

    protected override IEnumerator ExecuteAbility()
    {
        owner.gameplayTagSystem.AddTag(AbilityTag);
        while (true) yield return null;
        EndAbility();
    }
}