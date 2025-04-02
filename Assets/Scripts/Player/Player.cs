using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.EventSystems;
using static ItemData;

public partial class Player : MonoBehaviour , IPlayerserveice
{
    private IinputController iinputController;

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
    
    [SerializeField]
    private PlayerInventory inventory;
    private Door door;

    private void Start()
    {
        gameObject.tag = "Player"; 
        rb = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();
        iinputController = InputController.instance.GetComponent<IinputController>();
        iinputController.SubscribeToFKeyPress(OpentheDoor);
        iinputController.SubscribeToXKeyPress(AbilitySkillX);

    }
    public GameAbility currentAbility;
    Task abilityskill;
    private void Update()
    {
        Move();
        RotateToCameraDirection();
        DropItemUpdate();


    }

    private void OnDestroy()
    {
        iinputController.UnsubscribeFromFKeyPress(OpentheDoor);
        iinputController.UnsubscribeFromXKeyPress(AbilitySkillX);
    }

    private void OpentheDoor()
    {
        if (door != null)
            door.Open();
    }

    private void AbilitySkillX()
    {
        abilityskill = currentAbility.ActivateAbility();
    }


    /// <summary>
    /// 해당 코드는 이전에 사용했던 코드 이제 사용하지않지만 비교차원해서 남겨둠
    /// 가장큰 차이점은 linerarVelocity를 사용했을때는 감속으로 미끄러지는 이동이 남아있었지만
    /// SimpleMove의 속도는 입력이 없으면 바로 0 이된다.
    /// 다음에는 이런식으로 사용하지말자
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

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out DroppedItem item))
        {
            nearbyItems.Remove(item);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Door")
        {
            door = other.GetComponent<Door>();
        }
        else if(other.TryGetComponent(out DroppedItem item))
        {
            Debug.Log($"{other.gameObject.name}");
            nearbyItems.Add(item);
        }
    }


    public void SetPos(Vector3 vec3)
    {
        gameObject.transform.position = vec3;
    }

    public PlayerInventory GetPlayerInventory()
    {
        return inventory;
    }

    /// <summary>
    /// 이건 단순 테스트를 위해 만든 아이템 데이터 나중에수정필요
    /// </summary>


    public List<ItemData> itemlist = new List<ItemData>();

}
