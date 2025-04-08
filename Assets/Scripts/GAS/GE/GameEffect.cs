using UnityEngine;

public enum eModifier
{
    Add = 0,
    Multiply = 1,
    Equal,
}

/// <summary>
/// �������� �߻��Ǵ� ���� ������ �������ش�.
/// </summary>
public class GameEffect
{
    public SOGameAttributeData model;

    //����
    public eModifier modifierOp;

    protected IGameEffectExecutionCalculation execution;


    /// <summary>
    /// �����ڿ��� effect�� ����
    /// ������ ����� ��
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

    //���߿� �� �ڵ�� �ٲ���
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
