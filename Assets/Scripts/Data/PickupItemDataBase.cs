using UnityEngine;

public abstract class PickupItemDataBase : ScriptableObject
{
    public eEuipmentType eEquipmentType;
    public ItemData itemData;
    public eTagType tag;
    //발동될 스킬
    public GameObject gameAbility;
}

