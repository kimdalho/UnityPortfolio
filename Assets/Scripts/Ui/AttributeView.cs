using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AttributeView : MonoBehaviour
{
    public List<TextMeshProUGUI> tmps;

    public Player player;
    public void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }


    public void Update()
    {
        ShowData(player.attribute);
    }



    public void ShowData(GameAttribute model)
    {
        tmps[0].text = $"Hart {model.MaxHart} / {model.CurHart}";
        tmps[1].text = $"Atk {model.atk}";
        tmps[2].text = $"Speed {model.speed}";
    }

}
