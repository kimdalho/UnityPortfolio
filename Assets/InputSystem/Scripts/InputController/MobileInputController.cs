using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// 추후 인풋시스템으로 업데이트 할 수도 있다. 이식성을 위해 입력 이벤트를 관리하는 컨트롤러
/// </summary>
public class MobileInputController : InputControllerBase
{
    [Header("UI References")]
    public RectTransform joystickBG;
    public RectTransform joystickHandle;

    private bool isDragging = false;

    [Header("Raycast Settings")]
    public GraphicRaycaster uiRaycaster;
    public EventSystem eventSystem;

    [Header("Raycast")]
    public Camera mainCamera;

    private void OnEnable()
    {
        pointerAction = inputActions.Mobile.Pointer;
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

        Vector2 screenPos = pointerAction.ReadValue<Vector2>();

        if (isDragging)
        {
            if (IsPointerOverJoystick(screenPos))
            {
                UpdateJoystick(screenPos);
            }
            else
            {
                ResetJoystick();
            }
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

    bool IsPointerOverJoystick(Vector2 screenPosition)
    {
        PointerEventData pointerData = new PointerEventData(eventSystem)
        {
            position = screenPosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        uiRaycaster.Raycast(pointerData, results);

        foreach (var result in results)
        {
            if (result.gameObject == joystickBG.gameObject || result.gameObject.transform.IsChildOf(joystickBG))
                return true;
        }
        return false;
    }
}
