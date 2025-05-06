using System;
using System.Collections.Generic;
using UnityEngine;

public enum eModifier
{
    Add = 0,
    Multiply = 1,
    Equal = 2,
    Division = 3,
    Minus = 4,
    AddOnlyAttack = 5,
}

public enum eDurationPolicy
{
    None = 0,
    Buff = 1, //�ش� ����Ʈ�� �÷��̾� ������ �ξ���Ѵ�.
    Infinite = 2, //���������� �ø���. �̰� ���̽��� +�Ѵ�.
    HasDuration = 3, //���� �ð����ȸ� ���� �̰� CurrentValue�� �߰�
    Default = 4,
}



public struct AttributeModifier
{
    public eAttributeType attributeType;
    public float value;

    public AttributeModifier(eAttributeType type, float value)
    {
        attributeType = type;
        this.value = value;
    }

}


/// <summary>
/// �������� �߻��Ǵ� ���� ������ �������ش�.
/// </summary>
public class GameEffect 
{
    public List<AttributeModifier> modifiers = new List<AttributeModifier>();    
    public float duration = 0f;
    //����
    public eModifier modifierOp = eModifier.Add;
    public eDurationPolicy eDurationPolicy = eDurationPolicy.None;
    public GameEffect(eModifier modifier)
    {
        modifierOp = modifier;        
    }
    public void AddModifier(eAttributeType type, float value)
    {
        modifiers.Add(new AttributeModifier(type, value));
    }
}
