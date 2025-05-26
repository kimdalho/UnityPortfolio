using System.Collections.Generic;
using UnityEngine;

public class HeadItem : EquipmentItem
{
    public List<GameObject> models;
    public int objectIndex;
    public override void Init(PickupItemData data)
    {
        base.Init(data);
        models[data.objectIndex].gameObject.SetActive(true);
        objectIndex = data.objectIndex;
    }

    public override void OnPickup(Character source)
    {        
        base.OnPickup(source);
        source.GetModelController().SetActiveExclusive(partType, models[objectIndex]);
    }
}
