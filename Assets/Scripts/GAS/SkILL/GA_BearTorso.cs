using System;
using System.Collections;
using UnityEngine;
public class GA_BearTorso : GameAbility
{
    //beartorso, /        ���� �� 60% Ȯ���� ����ü 2�� �¿�� �߰� �߻�

    public int multypleCount = 2;
    public float condtionper = 0.6f;
    protected override IEnumerator ExecuteAbility()
    {
        owner.fxSystem.ExecuteFX(eTagType.Effect_NinjaSkill, owner.transform);
       
        var value = UnityEngine.Random.value;
        if (value < condtionper)
        {
            MulityShoot();
        }

        yield return null;
    }

    private void MulityShoot()
    {

        Character character = owner.GetComponent<Character>();
        if (character != null)
        {
            GameAbility ability = character.GetAbilitySystem().GetGameAbility(eTagType.Attack);
            if (ability != null)
            {
                IProjectileCountModifiable modifiable = ability.GetComponent<IProjectileCountModifiable>();
                modifiable.SetFireMultypleCount(eModifier.Add, multypleCount);
            }
        }
    }
}