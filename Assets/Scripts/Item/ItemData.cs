using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Game/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;       // 아이템 이름
    public string description;    // 아이템 설명
    public Sprite itemIcon;       // UI에서 사용할 아이콘
    public eItemType itemType;     // 아이템 종류 (포션, 장비 등)
    public int itemValue;         // 회복량 또는 공격력 증가량

    public enum eItemType
    {
        Consumable,  // 소비형 (포션 등)
        Equipment,   // 장비형 (검, 방어구)
        DroppedItem,  // 바닥에 떨어진 상태
        PlaceableBlock, // 맵에 배치된 상태
        UsableEntity, // 상자, 작업대처럼 동작하는 오브젝트
    }

    public ItemData(string itemName, string description, eItemType itemType, int itemValue) {
        this.itemName = itemName;
        this.description = description;
        this.itemType = itemType;
        this.itemValue = itemValue;
    }

}