using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    None,
    Punch,          // �⺻ ����
    HandGun,        // ����
    FlameThrower,   // ȭ�� ����
}

public class Weapon : MonoBehaviour
{
    private Dictionary<WeaponType, Transform> weapons = new Dictionary<WeaponType, Transform>();

    [SerializeField] private Transform handL;
    [SerializeField] private Transform handR;

    public Transform currentWeapon;

    private void Awake()
    {
        // ���� Object �ʱ�ȭ
        weapons.Add(WeaponType.HandGun, handL.GetChild(4));
        weapons.Add(WeaponType.FlameThrower, handL.GetChild(6));
    }

    public void SwapWeapon()
    {

    }

    public void ExecuteFireFX()
    {
        // �ѱ� ��ġ�� FX ����

    }
}
