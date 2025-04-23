using System.Collections;
using UnityEngine;

public class GA_GrassTrunk : GameAbility
{
    private void Awake()
    {
        AbilityTag = eTagType.grasstrunk;
    }

    protected override IEnumerator ExecuteAbility()
    {
        // �ѹ��� ���׷��̵� or ���������� ���� ���ݷ��� tracking�ؼ� ���� �÷���?
        owner.gameplayTagSystem.AddTag(AbilityTag);
        owner.attribute.atk = Mathf.RoundToInt(owner.attribute.atk * 1.5f);

        EndAbility();
        yield break;
    }
}