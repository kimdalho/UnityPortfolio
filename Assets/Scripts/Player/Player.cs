
using System.Collections.Generic;
using UnityEngine;

public partial class Player : Character
{
    
    [SerializeField]
    private InputController inputController;
    public float rotationSpeed = 10f;
    public Transform cameraTransform;
    private Vector3 moveDirection;

    [SerializeField]
    private ScanForTargets scanForTargets;

    Vector3 moveVec;
    
    private HashSet<Collider> detectedItems = new HashSet<Collider>();

    #region ������ ����â
    public Color gizmoColor = Color.red;
    public float scanRadius = 5f;
    public LayerMask itemLayer;
    public LayerMask monsterLayer;

    private Collider[] buffer = new Collider[4]; // �̸� �Ҵ�� ����
    public ItemdescriptionView itemdescriptionView;
    #endregion

    [SerializeField]
    private AbilitySystem abilitySystem;
 
    private void Start()
    {        
        if (abilitySystem == null)
        {
            Debug.LogError("AbilitySystem�� ã�� �� �����ϴ�!");
            return;
        }

        gameObject.tag = "Player";

        if (inputController == null)
        {
            Debug.LogError("InputController�� ã�� �� �����ϴ�!");
            return;
        }        
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
        //�ڼ���� ���ٱ� ������        
        DropItemUpdate();
        //������ ����â
        HasPickupablesNearby();
        //
        AutoAttack();
    }

    private void OnDestroy()
    {
        if (inputController == null) return;
    }


    bool isAttack;
    private void AbilitySkillAttack()
    {
        abilitySystem.ActivateAbility(eTagType.Attack, this);
    }

    private void AbilitySkillAttackEnd()
    {
        if (abilitySystem == null)
        {
            Debug.LogError("AbilitySystem�� �������� �ʾ� AbilitySkillAttackEnd() ���� �Ұ�!");
            return;
        }

        abilitySystem.DeactivateAbility(eTagType.Attack);
    }

    private void Move()
    {
        float hAxis = inputController.InputDirection.y;
        float vAxis = inputController.InputDirection.x;
        Vector3 move = new Vector3(vAxis, 0, hAxis);
        Debug.Log("hAxis " + hAxis + "vAxis " + vAxis);

        //moveDirection = attribute.speed * move;

        Vector3 forward = gameObject.transform.forward;
        Vector3 right = gameObject.transform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        moveDirection = forward * hAxis + right * vAxis;
        Debug.Log("moveDirection " + moveDirection);
        moveDirection *= attribute.speed;


        isGrounded = characterController.isGrounded;

        if (isGrounded && calcVelocity.y < 0)
        {
            calcVelocity.y = 0;
        }


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            calcVelocity.y += Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
        }

        calcVelocity.y += gravity * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime);
        characterController.Move(calcVelocity * Time.deltaTime);

        bool ismove = (move != Vector3.zero);
        animator.SetBool(moveHash, ismove);
    }

    void RotateToCameraDirection()
    {   
        if(scanForTargets.lookatMonster != null)
        {
            Vector3 direction = scanForTargets.lookatMonster.position - transform.position;
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
        else if (moveDirection.sqrMagnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void AutoAttack()
    {
        //Debug.Log(moveDirection);
        if (scanForTargets.lookatMonster != null && moveDirection == Vector3.zero && isAttack == false)
        {
            isAttack = true;
          
        }
        else if((scanForTargets.lookatMonster == null || moveDirection != Vector3.zero) && isAttack == true)
        {
            isAttack = false;
            AbilitySkillAttackEnd();
        }

        if(isAttack == true)
        {
            AbilitySkillAttack();
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


    #region ������ ����â
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

    /// <summary>
    /// ī�޶� �������� �÷��̾� �̵� ���� ���ϱ�
    /// �̷��� �ϸ� ���縵�̳� ����ó�� ī�޶�� �÷��̾��� ������ �����ִ�.
    /// </summary>
    /// <param name="vAxis"></param>
    /// <param name="hAxis"></param>
    private void SetPlayerMoveDirectionToCameraDirection(float vAxis, float hAxis)
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        moveDirection = forward * vAxis + right * hAxis;
        moveDirection *= attribute.speed;
    }

}

