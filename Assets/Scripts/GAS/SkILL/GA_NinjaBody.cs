using System.Collections;
using UnityEngine;


public class GA_NinjaBody : GameAbility
{

    public GameEffect GE_NinjaBodyspeedUp;

    public eTagType state = eTagType.NinjaBody_State_SpeedUp;

    protected override IEnumerator ExecuteAbility()
    {
        StartAbility();
        yield return null;
    }


    private void StartAbility()
    {
        owner.gameplayTagSystem.AddTag(AbilityTag);
        owner.Onkill += Onkill;
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("킬 테스트");
            owner.Onkill.Invoke();
        }

    }

    private void Onkill()
    {
        GE_NinjaBodyspeedUp.modifierOp = eModifier.Multiply;

        GE_NinjaBodyspeedUp.ApplyGameplayEffectToSelf(owner);
        StartCoroutine(OnkillProcess());
    }

    private IEnumerator OnkillProcess()
    {
        if (owner.gameplayTagSystem.HasTag(state) == true)
            yield break;
        owner.gameplayTagSystem.AddTag(state);

        Debug.Log("atk speed" + owner.attribute.attackSpeed);


        yield return new WaitForSeconds(Duration);
        EndOnkillProcess();
    }

    private void EndOnkillProcess()
    {
        GE_NinjaBodyspeedUp.modifierOp = eModifier.Division;
        GE_NinjaBodyspeedUp.ApplyGameplayEffectToSelf(owner);
        owner.gameplayTagSystem.RemoveTag(state);
    }




    
    public override void EndAbility()
    {
        base.EndAbility();
        GE_NinjaBodyspeedUp.modifierOp = eModifier.Division;


    }
}