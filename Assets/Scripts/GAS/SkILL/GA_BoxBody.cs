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
            // 어빌리티가 활성화 된 상태에서 공격을 진행 했다면 해당 어빌리티 태그 삭제
            //if (owner.gameplayTagSystem.HasTag(AbilityTag) && owner.gameplayTagSystem.HasTag(eTagType.Attack))
            //{
            //    owner.gameplayTagSystem.RemoveTag(AbilityTag);
            //    //yield return null;
            //}

            // 움직이면 어빌리티 활성화
            var _isMove = InputController.Instance.GetHorizontal() != 0 || InputController.Instance.GetVertical() != 0;
            if (_isMove && !owner.gameplayTagSystem.HasTag(AbilityTag))
                owner.gameplayTagSystem.AddTag(AbilityTag);

            yield return null;
        }
    }
}