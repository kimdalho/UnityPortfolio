using UnityEngine;

/// <summary>
/// 무기 어빌리티에 사용되는 데이터를 캐릭터 타입에게서 가져온다.
/// </summary>
public interface IWeaponTarget
{
    /// <summary>
    /// 총알이 발사할 방향을 가져온다.
    /// </summary>
    /// <returns></returns>
    public Vector3 GetTargetForward();
}