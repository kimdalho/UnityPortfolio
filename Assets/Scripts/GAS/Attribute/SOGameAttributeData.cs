using UnityEngine;

/// <summary>
/// �ʿ��� ���� ���� ���Ʈ ���� �̻��� ȿ���� ���� ���������� SO�� ������ �����ͷ� �������ִ´�.
/// </summary>

[CreateAssetMenu(fileName = "NewStatData", menuName = "Game/StatData")]
public class SOGameAttributeData :ScriptableObject
{
    public GameAttribute attribute;
}