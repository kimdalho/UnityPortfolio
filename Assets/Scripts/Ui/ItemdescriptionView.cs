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
        var pairdata = model.GetItemData();
        ItemData itemdata = pairdata.Item1;
        GameAttribute attribute = pairdata.Item2;

        if (itemdata == null || attribute == null)
            return;

        gameObject.SetActive(true);


        list[0].SetLocalizationID(itemdata.itemName);
        
        EnableCheck(list[1], attribute.MaxHart);
        EnableCheck(list[2], attribute.atk);
        EnableCheck(list[3], attribute.attackSpeed);
        EnableCheck(list[4], attribute.speed);
        
        list[5].SetLocalizationID(itemdata.description);

    }

    private void EnableCheck(LocalizedText tmp, float value)
    {
        tmp.gameObject.SetActive(value > 0);
        tmp.UpdateLocalizedText(value);
    }

}
