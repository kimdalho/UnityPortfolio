using System;
using UnityEngine;

/// <summary>
/// 추후 인풋시스템으로 업데이트 할 수도 있다. 이식성을 위해 입력 이벤트를 관리하는 컨트롤러
/// </summary>
public class InputController : MonoBehaviour
{
    [SerializeField] private float hAxis;
    [SerializeField] private float vAxis;

    public  Action OnFKeyPressed = () => { };
    public  Action OnXKeyPressed = () => { };
    public  Action OnLeftDown = () => { };
    public  Action OnLeftUp = () => { };

    public static InputController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F)) OnFKeyPressed.Invoke();
        if (Input.GetKeyDown(KeyCode.X)) OnXKeyPressed.Invoke();
        if (Input.GetMouseButtonDown(0)) OnLeftDown.Invoke();
        if (Input.GetMouseButtonUp(0)) OnLeftUp.Invoke();
    }

    public float GetHorizontal() => hAxis;
    public float GetVertical() => vAxis;

    // ✅ 제네릭을 활용한 Subscribe / Unsubscribe 통합
    public void Subscribe(ref Action eventAction, Action callback) => eventAction += callback;
    public void Unsubscribe(ref Action eventAction, Action callback) => eventAction -= callback;
}
