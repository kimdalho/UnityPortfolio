using UnityEngine;
using UnityEngine.InputSystem;

public abstract class InputControllerBase : MonoBehaviour, IInputEventProvider
{
    //############ IInputEventProvider ####################
    [Header("Input Action Asset")]
    [SerializeField] protected InputActionAsset _inputActions;

    public InputActionAsset inputActions
    {
        get => _inputActions;
        set => _inputActions = value;
    }

    public InputAction pointerAction { get; set; }

    public Vector2 InputDirection => inputVector;
    protected Vector2 inputVector;
    //############ IInputEventProvider ####################
}