/// <summary>
/// �ν��Ͻ����� �ݵ�� �ʱ�ȭ�� �ʿ��� Ŭ�������� ���
/// </summary>
public interface IInitializableItem<TDataInput>
{
    public void SetData(TDataInput data);
}