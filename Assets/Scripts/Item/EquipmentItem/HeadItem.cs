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
    }

    public override void OnPickup(Character source, GameObject picker)
    {        
        modifierOp = eModifier.Add;
        ApplyGameplayEffectToSelf(source, partType);
        source.GetModelController().SetActiveExclusive(eEuipmentType.Head, index);

        gameObject.SetActive(false);
    }
}
