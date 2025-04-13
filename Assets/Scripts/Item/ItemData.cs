using System;
using UnityEngine;

//[CreateAssetMenu(fileName = "NewItem", menuName = "Game/Item")]
//ScriptableObject
[System.Serializable]
public class ItemData
{
    public string itemName;       // ������ �̸�
    public string description;    // ������ ����    

    public ItemData(string itemName, string description, int itemValue) {
        this.itemName = itemName;
        this.description = description;        
        
    }

}