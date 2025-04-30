
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public interface IOnGameOver
{
    public void OnGameOver();
    
}


public partial class Player : Character , IOnGameOver ,IOnNextFlow
{
    #region 이동 컨트롤러
    [SerializeField]
    private InputController inputController;
    public float rotationSpeed = 10f;
    public Transform cameraTransform;
    private Vector3 moveDirection;
    private readonly float moveThresholdSqr = 0.01f;
    #endregion

    #region 룩엣

    [SerializeField]
    private ScanForTargets scanForTargets;
    [SerializeField]
    private CinemachineCamera lookatCam;
    #endregion


    private HashSet<Collider> detectedItems = new HashSet<Collider>();

    #region 아이템 설명창
    public Color gizmoColor = Color.red;
    public float scanRadius = 5f;
    public LayerMask itemLayer;
    public LayerMask monsterLayer;

    private Collider[] buffer = new Collider[4]; // 미리 할당된 버퍼
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




    private void Start()
    {        
        if (abilitySystem == null)
        {
            Debug.LogError("AbilitySystem을 찾을 수 없습니다!");
            return;
        }

        gameObject.tag = "Player";

        if (inputController == null)
        {
            Debug.LogError("InputController를 찾을 수 없습니다!");
            return;
        }        
    }

    private void Update()
    {
        Move();

        RotateToCameraDirection();
        //자석기능 없앨까 생각중        
        DropItemUpdate();
        //아이템 상태창
        HasPickupablesNearby();
        //
        ActivateAbilityAttack();
    }


    private void ActivateAbilityAttack()
    {
        if (gameplayTagSystem.HasTag(eTagType.Player_State_HasAttackTarget) is false)
        {            
            return;
        }            

        abilitySystem.ActivateAbility(eTagType.Attack, this);
    }

    public void AbilitySkillAttackEnd()
    {
        abilitySystem.DeactivateAbility(eTagType.Attack);
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

    

    void RotateToCameraDirection()
    {
        bool isMoving = moveDirection.sqrMagnitude > moveThresholdSqr; // moveThresholdSqr = 0.01f 정도 미리 정의
        if (scanForTargets.lookatMonster != null)
        {
            bool hasMultipleTargets = scanForTargets.m_TargetGroup.Targets.Count >= 2;
            if (hasMultipleTargets)
            {
                lookatCam.Priority = 2;
                RotateTowardsHorizontal(scanForTargets.lookatMonster.position - transform.position);
            }
            return;
        }       
        else if (isMoving)
        {
            lookatCam.Priority = 0;
            RotateTowardsHorizontal(moveDirection);
        }
    }

    void RotateTowardsHorizontal(Vector3 direction)
    {
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.001f) // 거의 0에 가까우면 무시
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

    /// <summary>
    /// 카메라 방향으로 플레이어 이동 방향 정하기
    /// 이렇게 하면 엘든링이나 몬헌처럼 카메라로 플레이어의 정면을 볼수있다.
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
        moveDirection *= attribute.speed;
    }

    public void OnGameOver()
    {
        animator.SetTrigger(DeadHash);
    }

    public const float delay = 5.0f;

    public void PortalDelay()
    {
        StartCoroutine(CoPortalDelay());
    }

    private IEnumerator CoPortalDelay()
    {
        gameplayTagSystem.AddTag(eTagType.Player_State_IgnorePortal);
        float deltime = 0f;
        while(deltime < delay)
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
}

