using System;
using UnityEngine;
using static Unity.VisualScripting.Member;

public enum eModifier
{
    Add = 0,
    Multiply = 1,
    Equal,
}

/// <summary>
/// �������� �߻��Ǵ� ���� ������ �������ش�.
/// </summary>
public class GameEffect : MonoBehaviour ,IGameEffect
{
    public GameAttribute effect = new GameAttribute();
    //����
    public eModifier modifierOp = eModifier.Add;

    protected IGameEffectExecutionCalculation execution;
    
    public virtual void ApplyGameplayEffectToSelf(Character source, eEuipmentType type)
    {
        source.attribute = GetFinalAttribute(source.attribute);

        Debug.Log($"{source.attribute.CurHart} {source.attribute.atk} {source.attribute.speed} {source.attribute.attackSpeed}");
    }

    public virtual void ApplyGameplayEffectToSelf(Character source)
    {
        GetFinalAttribute(source.attribute);
        Debug.Log($"{source.attribute.CurHart} {source.attribute.atk} {source.attribute.speed} {source.attribute.attackSpeed}");
    }

    public virtual GameAttribute ApplyGameplayEffectToSelf(GameAttribute attribute)
    {
       return GetFinalAttribute(attribute);
    }

    private GameAttribute GetFinalAttribute(GameAttribute attribute)
    {
        switch (modifierOp)
        {
            case eModifier.Multiply:
                attribute *= effect;
                break;
            case eModifier.Add:
                attribute += effect;
                break;
            case eModifier.Equal:
                attribute = effect;
                break;
        }
        return attribute;
    }

    public GameEffect(eModifier eModifier = eModifier.Add)
    {
        modifierOp = eModifier;
    }
    public GameEffect(IGameEffectExecutionCalculation execution)
    {
        this.execution = execution;
    }

    public void Apply(Character source, AttributeEntity target)
    {
        execution.Execute(source, target);
    }
}
