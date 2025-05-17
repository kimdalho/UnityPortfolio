using UnityEngine;
using UnityEngine.InputSystem;

public interface IInputEventProvider
{
     //ÀÎÇ²¾×¼Ç
     InputActionAsset inputActions { get; set; }
     InputAction pointerAction { get; set; }
     public Vector2 InputDirection { get; }
}