using UnityEngine;
using static Unity.VisualScripting.Member;

public abstract class BaseItem : MonoBehaviour, IPickupable
{
    protected ItemData itemData;
    public AttributeSet attributeSet;
    [SerializeField]
    protected GameObject abilityPrefab;
    [SerializeField]
    protected eTagType skilltag;
    protected eEuipmentType partType;
    protected int rank;
    public (ItemData, AttributeSet) GetItemData()
    {
        return (itemData, attributeSet);
    }

    public virtual void Init(PickupItemData data)
    {
        abilityPrefab = data.gameAbility;        
        itemData = data.itemData;
        attributeSet = data.attribute;
        partType = data.eEquipmentType;
        skilltag = data.tag;
    }

    public virtual void OnPickup(Character character)
    {
        ItemApplyEffect(character, this);
    }

    public virtual void ItemApplyEffect(Character source, BaseItem item)
    {
        var itemBaseHealth = item.attributeSet.GetBaseValue(eAttributeType.Health);
        var itemBaseAttack = item.attributeSet.GetBaseValue(eAttributeType.Attack);
        var itemBaseAttackSpeed = item.attributeSet.GetBaseValue(eAttributeType.AttackSpeed);
        var itemBaseSpeed = item.attributeSet.GetBaseValue(eAttributeType.Speed);

        GameEffect effect = new GameEffect(eModifier.Add);
        effect.AddModifier(eAttributeType.Health, itemBaseHealth);
        effect.AddModifier(eAttributeType.Attack, itemBaseAttack);
        effect.AddModifier(eAttributeType.AttackSpeed, itemBaseAttackSpeed);
        effect.AddModifier(eAttributeType.Speed, itemBaseSpeed);
        source.ApplyEffect(effect);
        gameObject.SetActive(false);
    }

    public GameObject GetGabilityPrefab()
    {
        return abilityPrefab;
    }
}