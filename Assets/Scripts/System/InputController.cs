using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 추후 인풋시스템으로 업데이트 할 수도 있다. 이식성을 위해 입력 이벤트를 관리하는 컨트롤러
/// </summary>
public class InputController : MonoBehaviour
{

    [Header("UI References")]
    public RectTransform joystickBG;
    public RectTransform joystickHandle;

    [Header("Input Action Asset")]
    public InputActionAsset inputActions;
    private InputAction pointerAction;

    private Vector2 inputVector;
    public Vector2 InputDirection => inputVector;

    private bool isDragging = false;

    private readonly string ActionMap_Joystick = "Joystick";
    private readonly string Action_Pointer = "Pointer";


    private void OnEnable()
    {
        pointerAction = inputActions.FindActionMap("Joystick").FindAction("Pointer");
        pointerAction.Enable();
    }

    private void OnDisable()
    {
        pointerAction.Disable();
    }

    private void Update()
    {
        if (Touchscreen.current != null)
        {
            isDragging = Touchscreen.current.primaryTouch.press.isPressed;
        }
        else
        {
            isDragging = Mouse.current.leftButton.isPressed;
        }

        if (isDragging)
        {
            Vector2 screenPos = pointerAction.ReadValue<Vector2>();
            UpdateJoystick(screenPos);
        }
        else
        {
            ResetJoystick();
        }
    }

    void UpdateJoystick(Vector2 screenPosition)
    {
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystickBG, screenPosition, null, out localPoint))
        {
            Vector2 normalized = new Vector2(
                (localPoint.x / joystickBG.sizeDelta.x) * 2,
                (localPoint.y / joystickBG.sizeDelta.y) * 2);

            inputVector = normalized.magnitude > 1 ? normalized.normalized : normalized;


            joystickHandle.anchoredPosition = new Vector2(
                inputVector.x * joystickBG.sizeDelta.x / 2,
                inputVector.y * joystickBG.sizeDelta.y / 2);
        }
    }

    void ResetJoystick()
    {
        inputVector = Vector2.zero;
        joystickHandle.anchoredPosition = Vector2.zero;
    }


    public void Keyboard()
    {
        #region 마우스 셋업
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        #endregion
    }

}
