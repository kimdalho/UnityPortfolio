using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
public class ItemdescriptionView : MonoBehaviour
{
    public Player player;
    public List<TextMeshProUGUI> list;




    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        gameObject.SetActive(false);
    }


    public void SetData(IPickupable model)
    {
        gameObject.SetActive(true);
        var pairdata = model.GetItemData();
        ItemData itemdata =  pairdata.Item1;
        GameAttribute attribute = pairdata.Item2;

        list[0].text = $"{itemdata.itemName}";
        list[1].text = $"Attack Up 1 {attribute.atk}";
        list[2].text = $"Speed 1 {attribute.speed}";
        list[3].text = $"Max Heart Up 1 {attribute.MaxHart}";
        list[4].text = "투사체가 라이플로 바뀝니다.";
    }
}
