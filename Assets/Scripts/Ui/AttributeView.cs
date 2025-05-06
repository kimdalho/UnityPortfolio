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

    public void ShowHart(AttributeSet model)
    {
       var MaxHart = model.GetMaxValue(eAttributeType.Health);
       var CurHart = model.GetCurValue(eAttributeType.Health);
       for (int i = 0; i < hearts.Count; i++) 
       {
            hearts[i].ShowMaxHeart(MaxHart > i);
       }

        for (int i = 0; i < MaxHart; i++)
        {
            hearts[i].ShowCurrentHeart(CurHart > i);            
        }
    }

    public void ShowData(AttributeSet model)
    {
        localized[0].UpdateLocalizedText(model.GetCurValue(eAttributeType.Attack));
        localized[1].UpdateLocalizedText(model.GetCurValue(eAttributeType.AttackSpeed));
        localized[2].UpdateLocalizedText(model.GetCurValue(eAttributeType.Speed));
    }
}
