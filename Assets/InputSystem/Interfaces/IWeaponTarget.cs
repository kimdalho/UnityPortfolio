using UnityEngine;

/// <summary>
/// ���� �����Ƽ�� ���Ǵ� �����͸� ĳ���� Ÿ�Կ��Լ� �����´�.
/// </summary>
public interface IWeaponTarget
{
    /// <summary>
    /// �Ѿ��� �߻��� ������ �����´�.
    /// </summary>
    /// <returns></returns>
    public Vector3 GetTargetForward();
}