using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AttributeView : MonoBehaviour
{
    public List<LocalizedText> localized;

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
        localized[0].UpdateLocalizedText(model.atk);
        localized[1].UpdateLocalizedText(model.attackSpeed);
        localized[2].UpdateLocalizedText(model.speed);
    }
}
