using UnityEngine;
using UnityEngine.InputSystem;

public class PCInputController : InputControllerBase
{
    public void Keyboard()
    {
        #region ���콺 �¾�
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        #endregion
    }
}