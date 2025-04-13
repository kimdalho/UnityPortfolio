using UnityEngine;

public class AssaultRifle : EquipmentItem
{
    public override void OnPickup(Character source, GameObject picker)
    {
        ModelUpdate(source);

        modifierOp = eModifier.Add;
        ApplyGameplayEffectToSelf(source, partType);
        gameObject.SetActive(false);
    }

    public void ModelUpdate(Character source)
    {
        source.GetAnimator().runtimeAnimatorController = ResourceManager.Instance.dic[eWeaponType.Rifl];
        source.GetModelController().SetWeaponByIndex(eWeaponType.Rifl);
    }


}