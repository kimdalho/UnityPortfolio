using System;
using UnityEngine;

//[CreateAssetMenu(fileName = "NewItem", menuName = "Game/Item")]
//ScriptableObject
[System.Serializable]
public class ItemData
{
    public string itemName;       // 아이템 이름
    public string description;    // 아이템 설명
}