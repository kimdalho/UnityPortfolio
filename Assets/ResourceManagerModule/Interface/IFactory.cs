using UnityEngine;
/// <summary>
/// �ν��Ͻ��� �����ϴ� ������Ʈ
/// TInput : �����Ǿ��� ������ Ŭ����
/// TOutput : ��ȯ�Ǵ� Ŭ����
/// </summary>
public interface IFactory<TDataInput, TOutput>
{
    TOutput Create(TDataInput data, Transform parent);
}