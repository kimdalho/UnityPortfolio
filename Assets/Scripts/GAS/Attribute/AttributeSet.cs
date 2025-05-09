using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;


//base는 참조할 대상이다.
//해당 예시로부터
//플레이어의 공격력을 20% 영구적 증가시키는 효과  베이스를 참조 하고 current를 증가
//플레이어의 공격력을 10%를 3초간 증가시키는 효과 베이스를 참조하고 current를 증가
//즉 베이스는 최종(currnet) value가 얼마만큼 스킬들에 영향을 받을지를 계산할 참조대상의 값이다.


//current Value는 스킬 버프,디버프 효과에 따라 base보다 낮거나 높을수있다.
//하지만 적용받은 모든 스킬들이 릴리즈된다면 base와 값은 동일해야한다.

[System.Serializable]
public class GameAttribute
{
    [SerializeField]
    private string AttributeName;
    [SerializeField]
    private float MaxValue;
    [SerializeField]
    private float BaseValue;
    [SerializeField]
    private float CurValue;

    public GameAttribute(string _name,float maxvalue,float basevalue)
    {
        AttributeName = _name;
        MaxValue = maxvalue;
        BaseValue = basevalue;
        CurValue = basevalue;   
    }


    public void SetValue(float maxvalue, float basevalue)
    {
        MaxValue = maxvalue;
        BaseValue = basevalue;
        CurValue =  BaseValue;
    }
    public void SetValue(float basevalue)
    {
        BaseValue = Mathf.Clamp(basevalue, 0, MaxValue);
        CurValue = BaseValue;
    }

    public void Modify(float value , eModifier modifierOp)
    {
        switch(modifierOp)
        {
            case eModifier.Add:
                BaseValue += value;
                CurValue += value;
                break;
            case eModifier.Multiply:
                BaseValue *= value;
                CurValue *= value;
                break;
        }      
    }

    public void RemoveModify(float value, eModifier modifierOp)
    {
        switch (modifierOp)
        {
            case eModifier.Add:
                BaseValue -= value;
                CurValue -= value;
                break;
            case eModifier.Multiply:
                BaseValue /= value;
                CurValue /= value;
                break;
        }
    }    


    public float GetBaseValue()
    {
        return BaseValue;
    }

    public float GetMaxValue()
    {
        return MaxValue;
    }

    public float GetCurrentValue()
    {
        return CurValue;
    }
}

public enum eAttributeType
{
    Health = 0,
    Attack = 1,
    AttackSpeed = 2,
    Speed = 3,
}


[System.Serializable]
public class AttributeSet
{
    /// <summary>
    /// 인스펙터에서 수치 확인용
    /// 몬스터 SO에서는 해당 어트리뷰트로 셋업한다.
    /// </summary>
    [SerializeField]
    GameAttribute Health;
    [SerializeField]
    GameAttribute Attack;
    [SerializeField]
    GameAttribute AttackSpeed;
    [SerializeField]
    GameAttribute Speed;



    private Dictionary<eAttributeType, GameAttribute> attributes = new Dictionary<eAttributeType, GameAttribute>();

    //모든 스탯의 최대 수치값

    public AttributeSet()
    {
        foreach (eAttributeType attributetype in Enum.GetValues(typeof(eAttributeType)))
        {            
            attributes.Add(attributetype, new GameAttribute(attributetype.ToString(), 10, 0));
        }

        Health = attributes[eAttributeType.Health];
        Attack = attributes[eAttributeType.Attack];
        AttackSpeed = attributes[eAttributeType.AttackSpeed];
        Speed = attributes[eAttributeType.Speed];
    }

    public AttributeSet(AttributeSet basedata, params eAttributeType[] attributeTypes)
    {
        foreach (eAttributeType attributetype in Enum.GetValues(typeof(eAttributeType)))
        {
            attributes.Add(attributetype, new GameAttribute(attributetype.ToString(), 10, 0));
        }

        Health = attributes[eAttributeType.Health];
        Attack = attributes[eAttributeType.Attack];
        AttackSpeed = attributes[eAttributeType.AttackSpeed];
        Speed = attributes[eAttributeType.Speed];


        foreach (eAttributeType type in attributeTypes)
        {
            SetAttribute(type, basedata.GetAttribute(type));
        }
    }

    private void SetAttribute(eAttributeType type , GameAttribute basedata)
    {


        if (attributes.ContainsKey(type))
        {
            attributes[type] = basedata;
        }
        else
        {
            
        }
    }

    public GameAttribute GetAttribute(eAttributeType type)
    {
        if (attributes.ContainsKey(type))
        {
            return attributes[type];
        }
        Debug.LogError("GetAttribute Not Found");
        return null;
    }


    public float GetCurValue(eAttributeType type)
    {
        if (attributes.ContainsKey(type))
        {
            return attributes[type].GetCurrentValue();
        }
        Debug.LogError("AttributeSet Not Found");
        return -1;
    }

    public float GetMaxValue(eAttributeType type)
    {
        if (attributes.ContainsKey(type))
        {
            return attributes[type].GetMaxValue();
        }
        Debug.LogError("AttributeSet Not Found");
        return -1;
    }
    public float GetBaseValue(eAttributeType type)
    {
        if (attributes.ContainsKey(type))
        {
            return attributes[type].GetMaxValue();
        }
        Debug.LogError("AttributeSet GetBaseValue Not Found");
        return -1;
    }

    //왠만하면 사용 없어야한다 실질적으로는 GE를 사용해야하는게 모듈 원칙이다.
    public void SetValue(eAttributeType type ,float maxValue,float curValue)
    {
        if (attributes.ContainsKey(type))
        {
           attributes[type].SetValue(maxValue, curValue);
           return;
        }
        Debug.LogError("AttributeSet SetCurrentValue Not Found");
    }

    public void SetValue(eAttributeType type, float curValue)
    {
        if (attributes.ContainsKey(type))
        {
            attributes[type].SetValue(curValue);
            return;
        }
        Debug.LogError("AttributeSet SetCurrentValue Not Found");
    }

    public void SetCurrentValue(eAttributeType type, float curValue)
    {
        if (attributes.ContainsKey(type))
        {
            attributes[type].SetValue(curValue);
            return;

        }
        Debug.LogError("AttributeSet SetCurrentValue Not Found");
    }

    public void Modify(eAttributeType type,float value, eModifier modifierOp)
    {
        if (attributes.ContainsKey(type))
        {
            attributes[type].Modify(value, modifierOp);
            return;

        }
        Debug.LogError("AttributeSet Modify Not Found");
    }

    public void RemoveModify(eAttributeType type, float value, eModifier modifierOp)
    {
        if (attributes.ContainsKey(type))
        {
            attributes[type].RemoveModify(value, modifierOp);
            return;
        }
        Debug.LogError("AttributeSet Modify Not Found");
    }
}