using UnityEngine;

public enum eModifier
{
    Add = 0,
    Multiply = 1,
    Equal,
}

/// <summary>
/// 전투에서 발생되는 스탯 연산을 서비스해준다.
/// </summary>
public class GameEffect
{
    public SOGameAttributeData model;

    //오퍼
    public eModifier modifierOp;

    protected IGameEffectExecutionCalculation execution;


    /// <summary>
    /// 시전자에게 effect를 시전
    /// 버프나 디버프 등
    /// </summary>
    public virtual void ApplyGameplayEffectToSelf(Character source)
    {
        switch(modifierOp)
        {
            case eModifier.Multiply:
                source.attribute *= model.attribute;
                break;
            case eModifier.Add:
                source.attribute += model.attribute;
                break;
        }
    }

    //나중에 위 코드로 바꾸자
    public virtual void ApplyGameplayEffectToSelf(Character source, SOGameAttributeData effect)
    {
        switch (modifierOp)
        {
            case eModifier.Multiply:
                source.attribute *= effect.attribute;
                break;
            case eModifier.Add:
                source.attribute += effect.attribute;
                break;
            case eModifier.Equal:
                source.attribute = effect.attribute;
                break;
        }
    }
    public GameEffect()
    {
    
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
