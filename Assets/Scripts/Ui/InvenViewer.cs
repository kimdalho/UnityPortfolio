using UnityEngine;

public class InvenViewer : MonoBehaviour
{

    public InventorySlot[] slots;
    public GameObject itemPrefab;
    public ItemData model;
    private void Awake()
    {
        slots = GetComponentsInChildren<InventorySlot>();
      
    }

    public void AddItem(ItemData itemData, int amount)
    {
        foreach (var slot in slots)
        {
            if (slot.HasItem && slot.ItemData == itemData)
            {
                Debug.Log("이곳으론 절대 안들어가나?");
                slot.AddAmount(amount);
                return;
            }
           
        }

        foreach (var slot in slots)
        {
            if (!slot.HasItem)
            {
                slot.SetItem(itemData, amount);
                return;
            }
        }
    }

}
