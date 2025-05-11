using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 몸통은 최대 체력 또는 이동속도
/// </summary>
public class BodyItem : EquipmentItem
{
    public List<GameObject> models;
    public int index;
    public GameObject meshObject;
    public override void Init(PickupItemData data)
    {
        base.Init(data);
        models[data.objectIndex].gameObject.SetActive(true);
        meshObject = models[data.objectIndex];
        index = data.modelIndex;
        partType = data.eEquipmentType;      
        skilltag = data.tag;
    }

    public override void OnPickup(Character source)
    {
        base.OnPickup(source);
        MeshFilter render = meshObject.GetComponent<MeshFilter>();
        source.GetModelController().SetActiveExclusive(partType, render.sharedMesh);
    }
}
