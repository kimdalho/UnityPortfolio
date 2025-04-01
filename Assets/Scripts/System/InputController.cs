using System;
using UnityEngine;


/// <summary>
/// 추후 인풋시스템으로 업데이트 할수도있다. 이식성 위해서 입력 이벤트를 관리하는 컨트롤러
/// </summary>
public class InputController : MonoBehaviour , IinputController
{
    [SerializeField]
    private float hAxis;
    [SerializeField]
    private float vAxis;

    public Action OnFKeyPressed;
    public static InputController instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.F))
        {
            // OnFKeyPressed 콜백 호출
            OnFKeyPressed?.Invoke();
           
        }
    }

    public float GetHorizontal()
    {
        return hAxis;
    }

    public float GetVertical()
    {
        return vAxis;
    }

    public void SubscribeToFKeyPress(Action callback)
    {
        OnFKeyPressed += callback;
    }

    // 구독 해제 메서드
    public void UnsubscribeFromFKeyPress(Action callback)
    {
        OnFKeyPressed -= callback;
    }
}
