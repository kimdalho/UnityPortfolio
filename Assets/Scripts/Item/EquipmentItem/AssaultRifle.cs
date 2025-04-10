using UnityEngine;

public class AssaultRifle : EquipmentItem
{
    public override void OnPickup(Character source, GameObject picker)
    {
        source.GetAnimator().runtimeAnimatorController = ResourceManager.Instance.dic[eWeaponType.Rifl];
        source.GetModelController().m_weapons[0].gameObject.SetActive(true);
        
        modifierOp = eModifier.Add;
        ApplyGameplayEffectToSelf(source, partType);
        gameObject.SetActive(false);
    } 
}