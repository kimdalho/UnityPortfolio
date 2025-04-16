/// <summary>
/// 캐릭터간의 스탯 연산을 서비스한다. 이때 중요한건 GE의 effect는 사용여부가 있을때도 있고 없을수도 있다.
/// </summary>
public interface IGameEffectExecutionCalculation
{
	public void Execute(Character source, AttributeEntity target);
	
}
/// <summary>
/// 단순한 고정된 정수를 연산을 서비스한다.
/// 아이템의 effect를 사용할때도 있고, GameEffectSelect는 동적 인스턴싱으로 사용한다.
/// </summary>
public interface IGameEffect
{
	public void ApplyGameplayEffectToSelf(Character source);
}