using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
public class ItemdescriptionView : MonoBehaviour
{
    public Player player;
    public List<LocalizedText> list;

    private void Start()
    {
        player = GameManager.instance.GetPlayer();
        gameObject.SetActive(false);
    }


    public void SetData(IPickupable model)
    {
        gameObject.SetActive(true);
        var pairdata = model.GetItemData();
        ItemData itemdata =  pairdata.Item1;
        GameAttribute attribute = pairdata.Item2;

        list[0].SetLocalizationID(itemdata.itemName);
        list[1].UpdateLocalizedText(attribute.MaxHart);
        list[2].UpdateLocalizedText(attribute.atk);        
        list[3].UpdateLocalizedText(attribute.attackSpeed);
        list[4].UpdateLocalizedText(attribute.speed);
        
    }
}
