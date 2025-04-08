using UnityEngine;

/// <summary>
/// 필요한 이유 버프 디버트 상태 이상의 효과에 사용될 스탯정보는 SO로 정적인 데이터로 가지고있는다.
/// </summary>

[CreateAssetMenu(fileName = "NewStatData", menuName = "Game/StatData")]
public class SOGameAttributeData :ScriptableObject
{
    public GameAttribute attribute;
}