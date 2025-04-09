using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ControllerCharacter : Character
{

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

        //중력 프로그래스
        calcVelocity.y += gravity * Time.deltaTime;

        characterController.Move(calcVelocity * Time.deltaTime);
        bool ismove = (move != Vector3.zero);
        animator.SetBool(moveHash, ismove);
        animator.SetBool(fallingHash, !isGrounded);

    }

}
