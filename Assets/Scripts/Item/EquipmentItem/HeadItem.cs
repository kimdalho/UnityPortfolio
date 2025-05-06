using System.Collections.Generic;
using UnityEngine;

public class HeadItem : EquipmentItem
{
    public List<GameObject> models;
    public int index;
    public override void Init(PickupItemData data)
    {
        base.Init(data);
        models[data.objectIndex].gameObject.SetActive(true);
        index = data.modelIndex;
    }

    public override void OnPickup(Character source)
    {        
        base.OnPickup(source);
        source.GetModelController().SetActiveExclusive(partType, index);
    }
}
