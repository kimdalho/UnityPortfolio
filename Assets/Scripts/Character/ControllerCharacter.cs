using Unity.VisualScripting;
using UnityEngine;

public class ControllerCharacter : MonoBehaviour
{
    public float speed = 5f;
    public float jumpHeight = 2f;

    public float gravity = -9.81f;


    private CharacterController characterController;

    private bool isGrounded = false;

    //저항력
    public Vector3 calcVelocity;

    private Vector3 inputDirection = Vector3.zero;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        isGrounded = characterController.isGrounded;
        if(isGrounded && calcVelocity.y < 0)
        {
            calcVelocity.y = 0;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        characterController.Move(move * Time.deltaTime * speed);

        if(move != Vector3.zero)
        {
            transform.forward = inputDirection;
        }
        // jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            calcVelocity.y += Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
        }

        //중력 프로그래스
        calcVelocity.y += gravity * Time.deltaTime;


        characterController.Move(calcVelocity * Time.deltaTime);

    }

}
