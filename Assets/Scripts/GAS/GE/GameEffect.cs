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
public class GameEffect : MonoBehaviour
{
    public GameAttribute effect;
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
                source.attribute *= effect;
                break;
            case eModifier.Add:
                source.attribute += effect;
                break;
            case eModifier.Equal:
                source.attribute = effect;
                break;
        } 
    }


    
    public virtual void ApplyGameplayEffectToSelf(Character source, eEuipmentType type)
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
