using UnityEngine;

/// <summary>
/// �������� �߻��Ǵ� ���� ������ �������ش�.
/// </summary>
public class GameEffectSelf : IGameEffect
{
    public GameAttribute effect;
    //����
    public eModifier modifierOp;

    protected IGameEffectExecutionCalculation execution;

    public virtual void ApplyGameplayEffectToSelf(Character source)
    {
        switch (modifierOp)
        {
            case eModifier.Multiply:
                source.attribute *= effect;
                break;
            case eModifier.Add:
                source.attribute += effect;
                break;
            case eModifier.Equal:
                source.attribute = effect;
                break;
        }

        Debug.Log($"{source.attribute.CurHart} {source.attribute.atk} {source.attribute.speed} {source.attribute.attackSpeed}");
    }


    public GameEffectSelf(eModifier eModifier = eModifier.Add)
    {
        modifierOp = eModifier;
        effect = new GameAttribute();

    }

    public GameEffectSelf(IGameEffectExecutionCalculation execution)
    {
        this.execution = execution;
    }

    public void Apply(Character source, AttributeEntity target)
    {
        execution.Execute(source, target);
    }
}
