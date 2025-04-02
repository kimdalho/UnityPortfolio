using System;
using UnityEngine;


/// <summary>
/// ���� ��ǲ�ý������� ������Ʈ �Ҽ����ִ�. �̽ļ� ���ؼ� �Է� �̺�Ʈ�� �����ϴ� ��Ʈ�ѷ�
/// </summary>
public class InputController : MonoBehaviour , IinputController
{
    [SerializeField]
    private float hAxis;
    [SerializeField]
    private float vAxis;
    [SerializeField]
    private float attack1;

    public Action OnFKeyPressed;
    public Action OnXKeyPressed;

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
        attack1 = Input.GetAxis("Fire1");
        if (Input.GetKeyDown(KeyCode.F))
        {
            // OnFKeyPressed �ݹ� ȣ��
            OnFKeyPressed?.Invoke();       
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            // OnFKeyPressed �ݹ� ȣ��
            OnXKeyPressed?.Invoke();
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

    // ���� ���� �޼���
    public void UnsubscribeFromFKeyPress(Action callback)
    {
        OnFKeyPressed -= callback;
    }

    public void SubscribeToXKeyPress(Action callback)
    {
        OnXKeyPressed += callback;
    }

    public void UnsubscribeFromXKeyPress(Action callback)
    {
        OnXKeyPressed -= callback;
    }

    public float GetMouseLeft()
    {
        return attack1;
    }
}
