using System.Collections;

public class GA_BoxBody : GameAbility
{
    public static readonly int IncreaseCount = 2;

    private void Awake()
    {
        AbilityTag = eTagType.boxbody;
    }

    protected override IEnumerator ExecuteAbility()
    {
        while (true)
        {
            // �����Ƽ�� Ȱ��ȭ �� ���¿��� ������ ���� �ߴٸ� �ش� �����Ƽ �±� ����
            //if (owner.gameplayTagSystem.HasTag(AbilityTag) && owner.gameplayTagSystem.HasTag(eTagType.Attack))
            //{
            //    owner.gameplayTagSystem.RemoveTag(AbilityTag);
            //    //yield return null;
            //}

            // �����̸� �����Ƽ Ȱ��ȭ
            var _isMove = InputController.Instance.GetHorizontal() != 0 || InputController.Instance.GetVertical() != 0;
            if (_isMove && !owner.gameplayTagSystem.HasTag(AbilityTag))
                owner.gameplayTagSystem.AddTag(AbilityTag);

            yield return null;
        }
    }
}