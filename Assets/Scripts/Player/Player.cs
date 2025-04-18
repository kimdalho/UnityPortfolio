using System.Collections.Generic;
using UnityEngine;

public partial class Player : Character
{
    float hAxis;
    float vAxis;

    private InputController inputController;

    public float rotationSpeed = 10f;
    public Transform cameraTransform;
    private Vector3 moveDirection;

    Vector3 moveVec;
    
    private HashSet<Collider> detectedItems = new HashSet<Collider>();

    #region 아이템 설명창
    public Color gizmoColor = Color.red;
    public float scanRadius = 5f;
    public LayerMask itemLayer;
    private Collider[] buffer = new Collider[4]; // 미리 할당된 버퍼
    public ItemdescriptionView itemdescriptionView;
    #endregion


    private AbilitySystem abilitySystem;


  

    private void Start()
    {

        abilitySystem = GameObject.Find("AbilitySystem")?.GetComponent<AbilitySystem>();
        if (abilitySystem == null)
        {
            Debug.LogError("AbilitySystem을 찾을 수 없습니다!");
            return;
        }

        gameObject.tag = "Player";

        inputController = InputController.Instance;
        if (inputController == null)
        {
            Debug.LogError("InputController를 찾을 수 없습니다!");
            return;
        }

        inputController.Subscribe(ref inputController.OnFKeyPressed, AbilitySkillF);
        inputController.Subscribe(ref inputController.OnXKeyPressed, AbilitySkillX);
        inputController.Subscribe(ref inputController.OnLeftDown, AbilitySkillAttack);
        inputController.Subscribe(ref inputController.OnLeftUp, AbilitySkillAttackEnd);

        
    }

    private void Update()
    {
        if(gameplayTagSystem != null)
        {
            var tagSystem = gameplayTagSystem;
            if (tagSystem.HasTag(eTagType.portalLock) == true)
                return;
        }

        Move();        
        RotateToCameraDirection();
        //자석기능 없앨까 생각중
        DropItemUpdate();
        //아이템 상태창
        HasPickupablesNearby();


    }

    private void OnDestroy()
    {
        if (inputController == null) return;

        inputController.Unsubscribe(ref inputController.OnFKeyPressed, AbilitySkillF);
        inputController.Unsubscribe(ref inputController.OnXKeyPressed, AbilitySkillX);
        inputController.Unsubscribe(ref inputController.OnLeftDown, AbilitySkillAttack);
        inputController.Unsubscribe(ref inputController.OnLeftUp, AbilitySkillAttackEnd);
    }

    private void AbilitySkillF()
    {
        /*if (door != null)
            door.Open();*/
    }

    private void AbilitySkillX()
    {
        //테스트용
        //abilitySystem.ActivateAbility(eTagType.Attack, this);
    }

    private void AbilitySkillAttack()
    {
        abilitySystem.ActivateAbility(eTagType.Attack, this);
    }

    private void AbilitySkillAttackEnd()
    {
        if (abilitySystem == null)
        {
            Debug.LogError("AbilitySystem이 존재하지 않아 AbilitySkillAttackEnd() 실행 불가!");
            return;
        }

        abilitySystem.DeactivateAbility(eTagType.Attack);
    }

    private void Move()
    {
        hAxis = inputController.GetHorizontal();
        vAxis = inputController.GetVertical();

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        moveDirection = forward * vAxis + right * hAxis;        
        moveDirection *= attribute.speed;     
        isGrounded = characterController.isGrounded;

        if (isGrounded && calcVelocity.y < 0)
        {
            calcVelocity.y = 0;
        }

        Vector3 move = new Vector3(hAxis, 0, vAxis);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            calcVelocity.y += Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
        }

        calcVelocity.y += gravity * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime);
        characterController.Move(calcVelocity * Time.deltaTime);

        bool ismove = (move != Vector3.zero);
        animator.SetBool(moveHash, ismove);
        //animator.SetBool(fallingHash, !isGrounded);

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
        if (detectedItems.Contains(other)) return;
        detectedItems.Add(other);

        if (other.TryGetComponent(out DroppedItem item))
        {
            Debug.Log($"{other.gameObject.name}");
            nearbyItems.Add(item);
        }

        IPickupable pickup = other.GetComponent<IPickupable>();
        if (pickup != null)
        {            
            pickup.OnPickup(this,gameObject);
        }
    }

    public void SetPos(Vector3 vec3)
    {
        gameObject.transform.position = vec3;
    }


    #region 아이템 설명창
    public void HasPickupablesNearby()
    {
        int count = Physics.OverlapSphereNonAlloc(transform.position, scanRadius, buffer, itemLayer);
        if(count  == 0 )
        {
            itemdescriptionView.gameObject.SetActive(false);
            return;
        }
        
        if (buffer[0] != null && buffer[0].TryGetComponent<IPickupable>(out var pickup))
        {             
            itemdescriptionView.SetData(pickup);
        }
    }
    #endregion

    public AbilitySystem GetAbilitySystem()
    {
        return abilitySystem;
    }

}

