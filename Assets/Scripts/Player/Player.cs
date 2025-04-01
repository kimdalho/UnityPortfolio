using Unity.VisualScripting;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public Transform cameraTransform;
    private CharacterController characterController;
    private Vector3 moveDirection;

    float hAxis;
    float vAxis;
    Vector3 moveVec;
    [SerializeField]
    private Rigidbody rb;
    public Vector3 vec;

    private IinputController iinputController;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();
        iinputController = InputController.instance.GetComponent<IinputController>();
    }
    public GameAbility currentAbility;
    private void Update()
    {
        Move();
        RotateToCameraDirection();
        if (Input.GetKeyDown(KeyCode.F))
        {
            if(door != null)
                door.GetComponent<Door>().InputyPress();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            _ = currentAbility.ActivateAbility();
        }
    }
    Door door;

    /// <summary>
    /// �ش� �ڵ�� ������ ����ߴ� �ڵ� ���� ������������� �������ؼ� ���ܵ�
    /// ����ū �������� linerarVelocity�� ����������� �������� �̲������� �̵��� �����־�����
    /// SimpleMove�� �ӵ��� �Է��� ������ �ٷ� 0 �̵ȴ�.
    /// </summary>
    private void OldMove()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        rb.linearVelocity += moveVec * moveSpeed * Time.deltaTime;
        //transform.position += moveVec * speed * Time.deltaTime;
        transform.LookAt(transform.position + moveVec);

    }


    private void Move()
    {
        hAxis = iinputController.GetHorizontal();
        vAxis = iinputController.GetVertical();

        //hAxis = Input.GetAxis("Horizontal");
        //vAxis = Input.GetAxis("Vertical");

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        moveDirection = forward * vAxis + right * hAxis;
        moveDirection *= moveSpeed;

        characterController.SimpleMove(moveDirection);
    }

    void RotateToCameraDirection()
    {
        if (moveDirection.sqrMagnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Door")
        {
            door = other.GetComponent<Door>();
        }

    }


    public void SetPos(Vector3 vec3)
    {
        gameObject.transform.position = vec3;
    }
}
