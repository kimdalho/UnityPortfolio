using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;


//base�� ������ ����̴�.
//�ش� ���÷κ���
//�÷��̾��� ���ݷ��� 20% ������ ������Ű�� ȿ��  ���̽��� ���� �ϰ� current�� ����
//�÷��̾��� ���ݷ��� 10%�� 3�ʰ� ������Ű�� ȿ�� ���̽��� �����ϰ� current�� ����
//�� ���̽��� ����(currnet) value�� �󸶸�ŭ ��ų�鿡 ������ �������� ����� ��������� ���̴�.


//current Value�� ��ų ����,����� ȿ���� ���� base���� ���ų� �������ִ�.
//������ ������� ��� ��ų���� ������ȴٸ� base�� ���� �����ؾ��Ѵ�.

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
    /// �ν����Ϳ��� ��ġ Ȯ�ο�
    /// ���� SO������ �ش� ��Ʈ����Ʈ�� �¾��Ѵ�.
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

    //��� ������ �ִ� ��ġ��

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

    //�ظ��ϸ� ��� ������Ѵ� ���������δ� GE�� ����ؾ��ϴ°� ��� ��Ģ�̴�.
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