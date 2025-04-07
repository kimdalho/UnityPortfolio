using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<DroppedItem> inventory = new List<DroppedItem>();
    public void AddItem(DroppedItem item)
    {
        inventory.Add(item);        
    }
}
