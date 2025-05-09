
using UnityEngine;

/// <summary>
/// 리소스 공급자 인터페이스
/// 해당 인터페이스를 상속받는 대상은 컨텐츠에 리소스를 제공하는 클래스 입니다.
/// </summary>

public interface IResourceProvider
{
    GameObject GetPrefab(string key);
}
