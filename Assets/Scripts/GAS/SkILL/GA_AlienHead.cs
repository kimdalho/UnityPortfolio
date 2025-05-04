using System.Collections;

//GA_AlienHead, 투사체 다발 증가
public class GA_AlienHead : GameAbility , IProjectileCountModifiable

{
    public int fireMultypleCount;

    public void SetFireMultypleCount(eModifier type, int count)
    {
        switch (type)
        {
            case eModifier.Add:
                fireMultypleCount += count;
                break;
        }
    }

    protected override IEnumerator ExecuteAbility()
    {
        owner.fxSystem.ExecuteFX(eTagType.Effect_NinjaSkill, owner.transform);
        Character character =  owner.GetComponent<Character>();
        if(character != null)
        {
           GameAbility ability  = character.GetAbilitySystem().GetGameAbility(eTagType.Attack);
           if (ability != null)
           {
                IProjectileCountModifiable modifiable = GetComponent<IProjectileCountModifiable>();
                modifiable.SetFireMultypleCount(eModifier.Add, fireMultypleCount);
           }
        }
        yield return null;
    }
}