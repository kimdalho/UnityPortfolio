using System.Collections;
using UnityEngine;
/// �ǰ� �� 10% Ȯ���� 1�ʰ� ����
/// </summary>
public class GA_NinjaHead : GameAbility
{
    private void Start()
    {
		AbilityTag = eTagType.ninjahead;
	}

    protected override IEnumerator ExecuteAbility()
    {
		while (true)
		{
			if (owner.gameplayTagSystem.HasTag(eTagType.Attack))
			{
				float chance = Random.value; // 0.0 ~ 1.0
				if (chance < 0.3f)
				{
					Debug.Log("ok");
				}
				else
				{
					Debug.Log("failed");
				}
			}
			yield return null;
		}
			
	}


}