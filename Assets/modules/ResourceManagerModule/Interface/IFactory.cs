using UnityEngine;
/// <summary>
/// 인스턴싱을 지원하는 오브젝트
/// TInput : 제공되야할 데이터 클래스
/// TOutput : 반환되는 클래스
/// </summary>
public interface IFactory<TDataInput, TOutput>
{
    TOutput Create(TDataInput data, Transform parent);
}