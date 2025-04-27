using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "GameData/Weapon Item Data")]
public class PickupWeaponItemData : PickupItemDataBase
{
    public eWeaponType type;
    public RuntimeAnimatorController runtimeAnimatorController;
}

