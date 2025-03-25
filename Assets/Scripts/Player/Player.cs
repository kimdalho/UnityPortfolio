using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;

    float hAxis;
    float vAxis;
    Vector3 moveVec;
    private void Update()
    {
        hAxis =  Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        transform.position += moveVec * speed * Time.deltaTime;
        transform.LookAt(transform.position + moveVec);

        if(Input.GetMouseButtonDown(0))
        {

        }
    }
}
