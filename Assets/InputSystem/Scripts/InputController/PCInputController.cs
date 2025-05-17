using UnityEngine;
using UnityEngine.InputSystem;

public class PCInputController : InputControllerBase
{
    public void Keyboard()
    {
        #region 마우스 셋업
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        #endregion
    }
}