
using UnityEngine;

/// <summary>
/// ���ҽ� ������ �������̽�
/// �ش� �������̽��� ��ӹ޴� ����� �������� ���ҽ��� �����ϴ� Ŭ���� �Դϴ�.
/// </summary>

public interface IResourceProvider
{
    GameObject GetPrefab(string key);
}
