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
    Buff = 1, //해당 이펙트를 플레이어 하위에 두어야한다.
    Infinite = 2, //영구적으로 올린다. 이건 베이스에 +한다.
    HasDuration = 3, //일정 시간동안만 증가 이건 CurrentValue에 추가
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
/// 전투에서 발생되는 스탯 연산을 서비스해준다.
/// </summary>
public class GameEffect 
{
    public List<AttributeModifier> modifiers = new List<AttributeModifier>();    
    public float duration = 0f;
    //오퍼
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
