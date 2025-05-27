/// <summary>
/// 인스턴싱이후 반드시 초기화가 필요한 클레스에게 상속
/// </summary>
public interface IInitializableItem<TDataInput>
{
    public void SetData(TDataInput data);
}