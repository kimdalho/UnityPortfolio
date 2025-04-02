using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<Item> inventory = new List<Item>();
    public void AddItem(Item item)
    {
        inventory.Add(item);
        Debug.Log($"æ∆¿Ã≈€ »πµÊ: {item.GetItemName()}");
    }
}
