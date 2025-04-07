using System;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[Serializable]
public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI amountText;
    private ItemData itemData;
    private int amount;

    public bool HasItem => itemData != null;
    public ItemData ItemData => itemData;
    DraggableItem currentItem;

    private void Start()
    {
        icon.gameObject.SetActive(false); 
    }

    public void SetItem(ItemData newItem, int newAmount)
    {               
        itemData = newItem;
        amount = newAmount;
        icon.gameObject.SetActive(amount > 0);
        //icon.sprite = newItem.itemIcon;
        //icon.enabled = true;
        UpdateAmountText();
    }

    public void AddAmount(int addAmount)
    {
        amount += addAmount;
        UpdateAmountText();
    }

    public void SplitItem()
    {
        if (amount > 1)
        {
            int half = amount / 2;
            amount -= half;
            UpdateAmountText();
            GameObject.Find("Inventory").GetComponent<InvenViewer>().AddItem(itemData, half);
            //FindObjectOfType<InventoryUI>().AddItem(itemData, half);
        }
    }

    public void RemoveItem()
    {
        itemData = null;
        amount = 0;
        icon.enabled = false;
        amountText.text = "";
    }

    private void UpdateAmountText()
    {
        amountText.text = amount > 1 ? amount.ToString() : "";
    }

    public void SwapItems(InventorySlot otherSlot)
    {
        ItemData tempItem = itemData;
        int tempCount = amount;

        SetItem(otherSlot.itemData, otherSlot.amount);
        otherSlot.SetItem(tempItem, tempCount);
    }
}
