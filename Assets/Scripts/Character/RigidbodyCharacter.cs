using Unity.VisualScripting;
using UnityEngine;

public class RigidbodyCharacter : MonoBehaviour
{
    public float speed = 5f;
    public float jumpHeight = 2f;
    public float dashDistance = 5f;

    private Rigidbody mrigidbody;

    private bool isGround = false;
    public LayerMask groundLayerMask;
    public float groundCheckDistance;

    private Vector3 inputDirection = Vector3.zero;

    private void Start()
    {
        mrigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CheckGroundStaus();

        inputDirection = Vector3.zero;
        inputDirection.x = Input.GetAxis("Horizontal");
        inputDirection.z = Input.GetAxis("Vertical");

        if(inputDirection != Vector3.zero)
        {
            transform.forward = inputDirection;
        }
        // jump input
        if (Input.GetButtonDown("Jump") && isGround)
        {
            //�߷°��� y�� 2�踦 ��Ʈ�� ��� ������ ���� �������� ���⵵
            Vector3 jumpVelocity = Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
            mrigidbody.AddForce(jumpVelocity, ForceMode.VelocityChange);
        }


        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //�뽬 ������ linearDamping���� Ȱ���Ͽ� �з����� �뽬����� ����ϱ����� Log�Լ��� Ȱ��
            Vector3 dashVelocity = Vector3.Scale(transform.forward,
            dashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * mrigidbody.linearDamping + 1)) / -Time.deltaTime),
            0,
            (Mathf.Log(1f / (Time.deltaTime * mrigidbody.linearDamping + 1)) / -Time.deltaTime)));

            mrigidbody.AddForce(dashVelocity, ForceMode.VelocityChange);
        }
    }

    private void FixedUpdate()
    {
        mrigidbody.MovePosition(mrigidbody.position + inputDirection * speed * Time.fixedDeltaTime);

     
    }

    void CheckGroundStaus()
    {
        RaycastHit hitInfo;

#if UNITY_EDITOR
        Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * groundCheckDistance));
#endif

        if(Physics.Raycast(transform.position + (Vector3.up * 0.1f),
            Vector3.down, out hitInfo,groundCheckDistance,groundLayerMask))
        {
            isGround = true;
        }
        else
        {
            isGround = false;   
        }
    }


}
