using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AttributeView : MonoBehaviour
{
    public List<LocalizedText> localized;

    public List<HeartIcon> hearts;

    public Player player;
    public void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void Update()
    {
        ShowHart(player.attribute);
        ShowData(player.attribute);
    }

    public void ShowHart(GameAttribute model)
    {       
       for (int i = 0; i < hearts.Count; i++) 
       {
            hearts[i].ShowMaxHeart(model.MaxHart > i);
       }

        for (int i = 0; i < model.MaxHart; i++)
        {
            hearts[i].ShowCurrentHeart(model.CurHart > i);            
        }
    }

    public void ShowData(GameAttribute model)
    {
        localized[0].UpdateLocalizedText(model.atk);
        localized[1].UpdateLocalizedText(model.attackSpeed);
        localized[2].UpdateLocalizedText(model.speed);
    }
}
