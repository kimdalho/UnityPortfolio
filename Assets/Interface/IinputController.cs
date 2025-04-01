
using System;

public interface IinputController
{
    public float GetHorizontal();
    public float GetVertical();

    /// <summary>
    /// FŰ ����
    /// </summary>
    /// <param name="callback"></param>
    public void SubscribeToFKeyPress(Action callback);
    /// <summary>
    /// FŰ ���� ����
    /// </summary>
    public void UnsubscribeFromFKeyPress(Action callback);

}
