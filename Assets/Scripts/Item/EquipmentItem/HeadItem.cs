using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class HeadItem : EquipmentItem
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
    }

    public override void OnPickup(Character source)
    {        
        base.OnPickup(source);
        var render = meshObject.GetComponent<MeshFilter>();
        ModelController model = source.GetModelController();
        model.SetActiveExclusive(partType, render.sharedMesh);
    }
}
