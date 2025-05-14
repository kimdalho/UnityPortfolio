using UnityEngine;

public class ControllerCharacter : Character
{

    private void Update()
    {
        isGrounded = characterController.isGrounded;
        if(isGrounded && calcVelocity.y < 0)
        {
            calcVelocity.y = 0;
            
        }
        
        Vector3 move = new Vector3(Input.GetAxis(GlobalDefine.String_Horizontal), 0, Input.GetAxis(GlobalDefine.String_Vertical));
        characterController.Move(move * Time.deltaTime * attribute.GetCurValue(eAttributeType.Speed));
      
        // jump input
        if (Input.GetButtonDown(GlobalDefine.String_Jump) && isGrounded)
        {
            calcVelocity.y += Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
        }

        //중력 프로그래스
        calcVelocity.y += gravity * Time.deltaTime;

        characterController.Move(calcVelocity * Time.deltaTime);
        //bool ismove = (move != Vector3.zero);
        //animator.SetBool(GlobalDefine.moveHash, ismove);
        //animator.SetBool(GlobalDefine.FallingHash, !isGrounded);

    }

}
