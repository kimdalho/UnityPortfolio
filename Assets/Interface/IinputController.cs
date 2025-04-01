
using System;

public interface IinputController
{
    public float GetHorizontal();
    public float GetVertical();

    /// <summary>
    /// F키 구독
    /// </summary>
    /// <param name="callback"></param>
    public void SubscribeToFKeyPress(Action callback);
    /// <summary>
    /// F키 구독 해제
    /// </summary>
    public void UnsubscribeFromFKeyPress(Action callback);

}
