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

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
        RotateToCameraDirection();
    }


    /// <summary>
    /// 해당 코드는 이전에 사용했던 코드 이제 사용하지않지만 비교차원해서 남겨둠
    /// 가장큰 차이점은 linerarVelocity를 사용했을때는 감속으로 미끄러지는 이동이 남아있었지만
    /// SimpleMove의 속도는 입력이 없으면 바로 0 이된다.
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
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");

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
}
