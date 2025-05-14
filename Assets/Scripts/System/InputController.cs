using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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

    [Header("Raycast Settings")]
    public GraphicRaycaster uiRaycaster;
    public EventSystem eventSystem;

    [Header("Raycast")]
    public Camera mainCamera;
    public LayerMask monsterLayer;

    [SerializeReference]
    private IPlayer controllerCharacter;
    [SerializeField] private MonoBehaviour playerMono; // 인스펙터 노출 

    float deltime;

    private void Awake()
    {
        controllerCharacter = playerMono as IPlayer;
    }

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
                RaycastToMonster(screenPos);
            }
        }
        else
        {
            ResetJoystick();
        }
        SetTargetDelay();
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


    private void SetTargetDelay()
    {
        if(deltime < 5)
        {
            deltime += Time.deltaTime;
        }
        else
        {
            deltime = 0;
        }
    }

    void ResetJoystick()
    {
        inputVector = Vector2.zero;
        joystickHandle.anchoredPosition = Vector2.zero;
    }
    /// <summary>
    /// 터치된 대상이 없으면 리셋
    /// </summary>
    /// <param name="screenPosition"></param>
    void RaycastToMonster(Vector2 screenPosition)
    {
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue, 0.3f);
           
        if (Physics.Raycast(ray.origin, ray.direction * 100, out RaycastHit hit, 100f, monsterLayer))
        {
            ILockOnTarget lockOnTarget =  hit.collider.gameObject.GetComponent<ILockOnTarget>();
            ;
            if (lockOnTarget != null && lockOnTarget.GetDead() == false)
            {
                controllerCharacter.SetPlayerTarget(lockOnTarget);                
                return;            
            }
        }
        controllerCharacter.ResetTarget();
    }

    public void Keyboard()
    {
        #region 마우스 셋업
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        #endregion
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
