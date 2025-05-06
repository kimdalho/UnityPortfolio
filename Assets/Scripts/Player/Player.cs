
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Cinemachine;
using UnityEngine;

public interface IOnGameOver
{
    public void OnGameOver();

}


public partial class Player : Character , IOnGameOver ,IOnNextFlow 
{
    #region �̵� ��Ʈ�ѷ�
    [SerializeField]
    private InputController inputController;
    public float rotationSpeed = 10f;
    public Transform cameraTransform;
    private Vector3 moveDirection;
    private readonly float moveThresholdSqr = 0.01f;
    #endregion
    private bool isDead;

    #region �迧

    [SerializeField]
    private ScanForTargets scanForTargets;
    [SerializeField]
    private CinemachineCamera lookatCam;
    #endregion


    private HashSet<Collider> detectedItems = new HashSet<Collider>();

    #region ������ ����â
    public Color gizmoColor = Color.red;
    public float scanRadius = 5f;
    public LayerMask itemLayer;
    public LayerMask monsterLayer;

    private Collider[] buffer = new Collider[4]; // �̸� �Ҵ�� ����
    public ItemdescriptionView itemdescriptionView;
    #endregion

    private void Awake()
    {
        GameManager.OnGameOver += OnGameOver;
        GameManager.OnNextFlow += OnNextFlow;
    }

    private void OnDestroy()
    {
        GameManager.OnGameOver -= OnGameOver;
        GameManager.OnNextFlow -= OnNextFlow;
    }


    //������ ���� ī��Ʈ
    [SerializeField] protected int fireCount = 1; // �߻� Ƚ��
    [SerializeField] protected int fireMultypleCount = 1;


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
        if (GetDead())
            return;

        Move();

        RotateToCameraDirection();
        //�ڼ���� ���ٱ� ������        
        DropItemUpdate();
        //������ ����â
        HasPickupablesNearby();
        //
        ActivateAbilityAttack();

        FallDeathCheck();
    }


    private void ActivateAbilityAttack()
    {
        if (gameplayTagSystem.HasTag(eTagType.Player_State_HasAttackTarget) is false)
        {            
            return;
        }            

        abilitySystem.ActivateAbility(eTagType.Attack, this);
    }

    public void OnJumpStart()
    {
        animator.SetTrigger("Trg_JumpStart");
    }

    public void OnFalling()
    {
        animator.SetBool(FallingHash, true);
    }

    public void OnEndJump()
    {
        animator.SetBool(FallingHash, false);
    }

    private void Move()
    {
        if (gameplayTagSystem.HasTag(eTagType.Player_State_IgnoreInput))
            return;

        float hAxis = inputController.InputDirection.x;
        float vAxis = inputController.InputDirection.y;
        Vector3 move = new Vector3(hAxis, 0, vAxis);
        SetPlayerMoveDirectionToCameraDirection(hAxis, vAxis);

        GroundCheck();
        if (isGrounded && calcVelocity.y < 0)
        {
            calcVelocity.y = 0;
            animator.SetBool(FallingHash, false);
        }
        else if(!isGrounded && calcVelocity.y > 0)
        {
            animator.SetBool(FallingHash, true);
        }

        calcVelocity.y += gravity * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime);
        characterController.Move(calcVelocity * Time.deltaTime);

        bool ismove = (move != Vector3.zero);
        animator.SetBool(moveHash, ismove);
    }

    public void MoveAnimStop()
    {
        animator.SetBool(moveHash, false);
    }

    public void FallDeathCheck()
    {
        if(transform.position.y < -8f)
        {
            GameManager.instance.GameOver();
        }
    }

    

    void RotateToCameraDirection()
    {
        bool isMoving = moveDirection.sqrMagnitude > moveThresholdSqr; // moveThresholdSqr = 0.01f ���� �̸� ����
        if (scanForTargets.lookatMonster != null)
        {          
            if (gameplayTagSystem.HasTag(eTagType.Player_State_HasAttackTarget))
            {
                lookatCam.Priority = 2;
                RotateTowardsHorizontal(scanForTargets.lookatMonster.position - transform.position);
            }
            return;
        }       
        else if (isMoving)
        {           
            RotateTowardsHorizontal(moveDirection);
        }
        lookatCam.Priority = 0;
    }

    void RotateTowardsHorizontal(Vector3 direction)
    {
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.001f) // ���� 0�� ������ ����
            return;

        Quaternion targetRotation = Quaternion.LookRotation(direction.normalized);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
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
            pickup.OnPickup(this);
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


    /// <summary>
    /// ī�޶� �������� �÷��̾� �̵� ���� ���ϱ�
    /// �̷��� �ϸ� ���縵�̳� ����ó�� ī�޶�� �÷��̾��� ������ �����ִ�.
    /// </summary>
    /// <param name="vAxis"></param>
    /// <param name="hAxis"></param>
    private void SetPlayerMoveDirectionToCameraDirection(float hAxis,float vAxis)
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        moveDirection = forward * vAxis + right * hAxis;
        moveDirection *= attribute.GetCurValue(eAttributeType.Speed);

        Debug.Log(moveDirection + "�ӵ�" + attribute.GetCurValue(eAttributeType.Speed));
    }

    public void OnGameOver()
    {
        isDead = true;
        gameplayTagSystem.AddTag(eTagType.Player_State_IgnoreInput);
        animator.Play(DeadHash);
    }

    public const float portalDelay = 5.0f;

    public void PortalDelay()
    {
        StartCoroutine(CoPortalDelay());
    }

    private IEnumerator CoPortalDelay()
    {
        gameplayTagSystem.AddTag(eTagType.Player_State_IgnorePortal);
        float deltime = 0f;
        while(deltime < portalDelay)
        {
            deltime += Time.deltaTime;
            yield return null;
        }
        gameplayTagSystem.RemoveTag(eTagType.Player_State_IgnorePortal);
    }

    public void OnNextFlow()
    {     
        gameplayTagSystem.AddTag(eTagType.Player_State_IgnoreInput);
        StartCoroutine(CoOnNextFlow());
    
    }

    private IEnumerator CoOnNextFlow()
    {
        gameplayTagSystem.AddTag(eTagType.Player_State_IgnoreInput);      
        yield return new WaitForSeconds(0.6f);
        transform.position = Vector3.zero;

        while (GameManager.Leveling)
        {
            yield return null;
        }
        gameplayTagSystem.RemoveTag(eTagType.Player_State_IgnoreInput);
    }

    public override bool GetDead()
    {
        return isDead;
    }
    public void SetPlayerTarget(Monster monster)
    {
        gameplayTagSystem.AddTag(eTagType.Player_State_HasAttackTarget);
        scanForTargets.SetPlayerTarget(monster);
    }

    public void ResetTarget()
    {
        
        scanForTargets.ResetTarget();
    }

    public void PlayAnimIdle()
    {
        animator.Play("FallingEnd");
    }
}

