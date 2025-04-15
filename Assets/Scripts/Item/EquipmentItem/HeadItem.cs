using System.Collections.Generic;
using UnityEngine;

public class HeadItem : EquipmentItem
{
    public List<GameObject> models;
    public int index;
    public override void Init(PickupItemData data)
    {
        models[data.objectIndex].gameObject.SetActive(true);
        index = data.modelIndex;
        partType = data.eEquipmentType;
        effect = data.attribute;
        skilltag = data.tag;
    }

    public override void OnPickup(Character source, GameObject picker)
    {        
        base.OnPickup(source, picker);
        source.GetModelController().SetActiveExclusive(partType, index);
    }
}
