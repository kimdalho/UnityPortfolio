using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ �ִ� ü�� �Ǵ� �̵��ӵ�
/// </summary>
public class BodyItem : EquipmentItem
{
    public List<GameObject> models;
    public int index;
    public override void Init(PickupItemData data)
    {
        base.Init(data);
        models[data.objectIndex].gameObject.SetActive(true);
        index = data.modelIndex;
        partType = data.eEquipmentType;      
        skilltag = data.tag;
    }

    public override void OnPickup(Character source)
    {
        base.OnPickup(source);
        source.GetModelController().SetActiveExclusive(partType, index);
    }
}
