using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PCInputController : InputControllerBase
{
    public Action Onfire;
    public void Keyboard()
    {
        #region 마우스 셋업
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        #endregion
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.PC.Fire.performed += ctx => Fire();
        inputActions.PC.Zoom.started += ctx => StartZoom();
        inputActions.PC.Zoom.canceled += ctx => StopZoom();
    }

    private void StopZoom()
    {
        Debug.Log("StopZoom");
    }

    private void StartZoom()
    {
        Debug.Log("StartZoom");
    }

    private void Fire()
    {
        Onfire?.Invoke();
    }  

    private void Update()
    {
        inputVector = inputActions.PC.Move.ReadValue<Vector2>();
        inputVector2 = inputActions.PC.Look.ReadValue<Vector2>();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

}