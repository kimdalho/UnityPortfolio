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
                Debug.Log("�̰����� ���� �ȵ���?");
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
