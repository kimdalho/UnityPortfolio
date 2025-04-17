using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 몸통은 최대 체력 또는 이동속도
/// </summary>
public class BodyItem : EquipmentItem
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
        ability = data.gameAbility;
    }

    public override void OnPickup(Character source, GameObject picker)
    {
        base.OnPickup(source, picker);
        source.GetModelController().SetActiveExclusive(partType, index);
    }
}
