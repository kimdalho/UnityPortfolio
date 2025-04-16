/// <summary>
/// ĳ���Ͱ��� ���� ������ �����Ѵ�. �̶� �߿��Ѱ� GE�� effect�� ��뿩�ΰ� �������� �ְ� �������� �ִ�.
/// </summary>
public interface IGameEffectExecutionCalculation
{
	public void Execute(Character source, AttributeEntity target);
	
}
/// <summary>
/// �ܼ��� ������ ������ ������ �����Ѵ�.
/// �������� effect�� ����Ҷ��� �ְ�, GameEffectSelect�� ���� �ν��Ͻ����� ����Ѵ�.
/// </summary>
public interface IGameEffect
{
	public void ApplyGameplayEffectToSelf(Character source);
}