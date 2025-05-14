using UnityEngine;

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

    public GameAttribute(string _name, float maxvalue, float basevalue)
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
        CurValue = BaseValue;
    }
    public void SetValue(float basevalue)
    {
        BaseValue = Mathf.Clamp(basevalue, 0, MaxValue);
        CurValue = BaseValue;
    }

    public void Modify(float value, eModifier modifierOp)
    {
        switch (modifierOp)
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
