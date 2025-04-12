using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 몸통은 최대 체력 또는 이동속도
/// </summary>
public class BodyItem : EquipmentItem
{
    public List<GameObject> models;
    public int index;
    public eTagType tagtype;
    public void Init(PickupItemData data)
    {
        models[data.objectIndex].gameObject.SetActive(true);
        index = data.modelIndex;
        tagtype = data.tag;
    }

    public override void OnPickup(Character source, GameObject picker)
    {
        source.gameplayTagSystem.AddTag(tagtype);
        modifierOp = eModifier.Add;
        ApplyGameplayEffectToSelf(source, partType);
        source.GetModelController().SetActiveExclusive(eEuipmentType.Body, index);

        gameObject.SetActive(false);
    }
}
