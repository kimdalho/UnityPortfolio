using UnityEngine;

public abstract class PickupItemDataBase : ScriptableObject
{
    public eEuipmentType eEquipmentType;
    public ItemData itemData;
    public eTagType tag;
    //�ߵ��� ��ų
    public GameObject gameAbility;
}

