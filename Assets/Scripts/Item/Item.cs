using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    private readonly String STR_PLAYER = "Player";
    private string itemName;


    public ItemData model;

    private void OnTriggerEnter(Collider other)
    {     
        if (other.CompareTag(STR_PLAYER))
        {
            PlayerInventory inventory = other.GetComponent<IPlayerserveice>().GetPlayerInventory();
            {
                if (inventory != null)
                {
                    gameObject.SetActive(false);
                    
                    inventory.AddItem(this);
                }
            }

            
        }
    }

    public string GetItemName() { return itemName; }

    public void SpawnDroppedItem()
    {
        

    }

}
