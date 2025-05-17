using UnityEngine;
using UnityEngine.InputSystem;

public interface IInputEventProvider
{
     //��ǲ�׼�
     InputActionAsset inputActions { get; set; }
     InputAction pointerAction { get; set; }
     public Vector2 InputDirection { get; }
}