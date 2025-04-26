using UnityEngine;

/// <summary>
/// 전투에서 발생되는 스탯 연산을 서비스해준다.
/// </summary>
public class GameEffectSelf : IGameEffect
{
    public GameAttribute effect;
    //오퍼
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
}
