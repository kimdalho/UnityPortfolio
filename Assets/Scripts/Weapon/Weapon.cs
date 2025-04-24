using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    None,
    Punch,          // 기본 공격
    HandGun,        // 권총
    FlameThrower,   // 화염 방사기
}

public class Weapon : MonoBehaviour
{
    private Dictionary<WeaponType, Transform> weapons = new Dictionary<WeaponType, Transform>();

    [SerializeField] private Transform handL;
    [SerializeField] private Transform handR;

    public Transform currentWeapon;

    private void Awake()
    {
        // 무기 Object 초기화
        weapons.Add(WeaponType.HandGun, handL.GetChild(4));
        weapons.Add(WeaponType.FlameThrower, handL.GetChild(6));
    }

    public void SwapWeapon()
    {

    }

    public void ExecuteFireFX()
    {
        // 총구 위치에 FX 생성

    }
}
