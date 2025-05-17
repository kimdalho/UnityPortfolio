using UnityEngine;
using UnityEngine.InputSystem;

public interface IInputEventProvider
{
    //��ǲ�׼�
     PlayerInputActions inputActions { get; set; }
     InputAction pointerAction { get; set; }     
     public Vector2 moveInputDirection { get; }
     public Vector2 lookInputDirection { get; }
}