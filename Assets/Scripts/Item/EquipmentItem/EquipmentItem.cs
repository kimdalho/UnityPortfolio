using System;
using UnityEngine;

public abstract class EquipmentItem : GameEffect, IPickupable
{    
    public ItemData itemData;
    //��� �������� ��� �������� Ÿ���� �������ִ�.
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
/// ��� Ÿ���� ����,�Ӹ�,�ٵ�,���� ������� ������ ȿ���� �������ִ�.
/// �ڽĿ��� ȿ���� �ο��ϴµ� �߿��Ѱ� �����Ƽ�� �ߵ��ؾ��Ѵٴ���
/// �׸��� �±׸� �������Ѵٴ���
/// Ư�� ���� ���� ���� �ִϸ��̼� ��ä�� �ٲ��.
/// </summary>


}


