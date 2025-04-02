using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.EventSystems;
using static ItemData;

public class Player : MonoBehaviour , IPlayerserveice
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
    Task task;
    private void Update()
    {
        Move();
        RotateToCameraDirection();
        
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
        task = currentAbility.ActivateAbility();
    }


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

    public PlayerInventory GetPlayerInventory()
    {
        return inventory;
    }

    /// <summary>
    /// �̰� �ܼ� �׽�Ʈ�� ���� ���� ������ ������ ���߿������ʿ�
    /// </summary>


    public List<ItemData> itemlist = new List<ItemData>();

    public void MakeItemData()
    {
        
        ItemData BlokWood = new ItemData(
            "����",
            "��ġ�� ����",
            eItemType.PlaceableBlock,
            1
            );

        ItemData wood = new ItemData(
        "����",
        "�����̴�",
         eItemType.DroppedItem,
        1
        );
        itemlist.Add(BlokWood);
        itemlist.Add(wood);

    }
}
