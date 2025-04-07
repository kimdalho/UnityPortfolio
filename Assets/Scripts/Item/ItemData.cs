using System;
using UnityEngine;

//[CreateAssetMenu(fileName = "NewItem", menuName = "Game/Item")]
//ScriptableObject
[SerializeField]
public class ItemData
{
    public string itemName;       // ������ �̸�
    public string description;    // ������ ����
    public Sprite itemIcon;       // UI���� ����� ������
    public eItemType itemType;     // ������ ���� (����, ��� ��)
    public int itemValue;         // ȸ���� �Ǵ� ���ݷ� ������

    public enum eItemType
    {
        Consumable,  // �Һ��� (���� ��)
        Equipment,   // ����� (��, ��)        
        UsableEntity, // ����, �۾���ó�� �����ϴ� ������Ʈ
        Resource,
    }

    public ItemData(string itemName, string description, eItemType itemType, int itemValue) {
        this.itemName = itemName;
        this.description = description;
        this.itemType = itemType;
        this.itemValue = itemValue;
    }

}