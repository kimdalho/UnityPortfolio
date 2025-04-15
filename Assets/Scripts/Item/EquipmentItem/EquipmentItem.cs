using System;
using UnityEngine;

public abstract class EquipmentItem : GameEffect, IPickupable
{    
    public ItemData itemData;
    //장비 아이템은 어느 파츠인지 타입을 가지고있다.
    public eEuipmentType partType;
    public eTagType skilltag;

    private Vector3 rotationSpeed = new Vector3(0, 30, 0);

    public (ItemData, GameAttribute) GetItemData() 
    {
        return (itemData, effect);
    }
    

    public virtual void OnPickup(Character source, GameObject picker) 
    {
        source.gameplayTagSystem.AddTag(skilltag);
        //modifierOp = eModifier.Add;
        ApplyGameplayEffectToSelf(source, partType);       
        gameObject.SetActive(false);

    }

    private void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }

    public virtual void Init(PickupItemData data) 
    {
    
    }


/// <summary>
/// 장비 타입은 무기,머리,바디,파츠 상관없이 고유한 효과를 가지고있다.
/// 자식에게 효과를 부여하는데 중요한건 어빌리티로 발동해야한다는점
/// 그리고 태그를 만들어야한다는점
/// 특히 무기 같은 경우는 애니메이션 전채가 바뀐다.
/// </summary>


}


