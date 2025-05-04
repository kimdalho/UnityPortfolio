using UnityEngine;

public class AssaultRifle : EquipmentItem
{

    protected eWeaponType eWeaponType;

    public override void Init(PickupWeaponItemData data)
    {
        base.Init(data);
        skilltag = eTagType.Attack;
        eWeaponType = data.type;
    }


    public override void OnPickup(Character source, GameObject picker)
    {
        Player player = source.GetComponent<Player>();
        var newskill = Instantiate(ability);
        var skillCompo = newskill.GetComponent<GameAbility>();
        skillCompo.gameObject.transform.SetParent(player.GetAbilitySystem().transform);
        player.GetAbilitySystem().AttackChange(skilltag, skillCompo);

        ModelUpdate(source);
        modifierOp = eModifier.Add;
        ApplyGameplayEffectToSelf(source, partType);
        gameObject.SetActive(false);
    }

    public void ModelUpdate(Character source)
    {
        source.GetAnimator().runtimeAnimatorController = itemAnim;
        source.GetModelController().SetWeaponByIndex(eWeaponType);
    }
}