using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.Rendering;

public class WeaponItem :BaseItem
{
    [SerializeField]
    protected eWeaponType eWeaponType;
    //애니메이션 정보
    public RuntimeAnimatorController itemAnim;
    public virtual void Init(PickupWeaponItemData data)
    {
        abilityPrefab = data.gameAbility;
        itemData = data.itemData;
        itemAnim = data.runtimeAnimatorController;
        partType = data.eEquipmentType;
        skilltag = data.tag;
        eWeaponType = data.type;
        rank = 3;
    }

    public override void OnPickup(Character source)
    {
        Player player = source.GetComponent<Player>();
        BaseItem item = GetComponent<BaseItem>();
        var newskill = Instantiate(abilityPrefab);
        var skillCompo = newskill.GetComponent<GameAbility>();
        skillCompo.gameObject.transform.SetParent(player.GetAbilitySystem().transform);
        player.GetAbilitySystem().AttackChange(skilltag, skillCompo);

        ModelUpdate(source);
        ItemApplyEffect(source, item);
        if(UserData.Instance != null)
            UserData.Instance.SetPickupedItem(rank);
    }

    public void ModelUpdate(Character source)
    {
        source.GetAnimator().runtimeAnimatorController = itemAnim;
        source.GetModelController().SetWeaponByIndex(eWeaponType);
    }

    public override void ItemApplyEffect(Character source, BaseItem item)
    {
        var itemBaseAttack = item.attributeSet.GetBaseValue(eAttributeType.Attack);

        GameEffect effect = new GameEffect(eModifier.Add);
        effect.AddModifier(eAttributeType.Attack, itemBaseAttack);

        source.ApplyEffect(effect);
        gameObject.SetActive(false);
    }
}