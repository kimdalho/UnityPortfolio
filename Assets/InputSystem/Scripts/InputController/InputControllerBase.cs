using UnityEngine;
using UnityEngine.InputSystem;

public abstract class InputControllerBase : MonoBehaviour, IInputEventProvider
{
    //############ IInputEventProvider ####################
    [Header("Input Action Asset")]
    [SerializeField] protected PlayerInputActions _inputActions;


    protected void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    public PlayerInputActions inputActions
    {
        get => _inputActions;
        set => _inputActions = value;
    }

    public InputAction pointerAction { get; set; }

    public Vector2 moveInputDirection => inputVector;
    protected Vector2 inputVector;
    public Vector2 lookInputDirection => inputVector2;
    protected Vector2 inputVector2;

    //############ IInputEventProvider ####################
}