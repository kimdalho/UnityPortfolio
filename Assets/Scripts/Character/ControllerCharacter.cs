using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ControllerCharacter : Character
{
    public float jumpHeight = 2f;

    public float gravity = -9.81f;


    private CharacterController characterController;
    private NavMeshAgent agent;


    private bool isGrounded = false;
    public LayerMask groundLayerMask;
    public float groundCheckDistance = 0.3f;


    //���׷�
    public Vector3 calcVelocity;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();
        //������Ʈ�� �̵������ ����
        agent.updatePosition = false;
        agent.updateRotation = false;

        attribute.speed = 5;
    }

    private void Update()
    {
        isGrounded = characterController.isGrounded;
        if(isGrounded && calcVelocity.y < 0)
        {
            calcVelocity.y = 0;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        characterController.Move(move * Time.deltaTime * attribute.speed);

        // jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            calcVelocity.y += Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
        }

        //�߷� ���α׷���
        calcVelocity.y += gravity * Time.deltaTime;


        characterController.Move(calcVelocity * Time.deltaTime);

    }

}
