using UnityEngine;
using UnityEngine.InputSystem.XInput;
using UnityEngine.UIElements;



public class PlayerControllerBase : Character , IPlayerController
{
    #region �̵� ��Ʈ�ѷ�
    [SerializeField]    
    private MonoBehaviour inputProviderRaw; // �ν����Ϳ� �Ҵ��� ��ü
    protected IInputEventProvider inputController;
    private float rotationSpeed = GlobalDefine.PlayerRotationBaseSpeed;
    private Vector3 moveDirection;
    private readonly float moveThresholdSqr = 0.01f;
    public Transform cameraTransform;
    #endregion

    private void Update()
    {
        if (GetDead())
            return;

        Move();

        RotateToCameraDirection();
    }

    protected override void Awake()
    {
        base.Awake();
        InputControllerSetup();
    }


    protected void InputControllerSetup()
    {
        if (inputProviderRaw == null)
            Debug.LogError("PlayerControllerBase => inputProviderRaw is Null");

        inputController = inputProviderRaw as IInputEventProvider;
    }




    protected void Move()
    {
        if (gameplayTagSystem.HasTag(eTagType.Player_State_IgnoreInput))
            return;

        float hAxis = inputController.moveInputDirection.x;
        float vAxis = inputController.moveInputDirection.y;

        controller.SetMoveDirection(hAxis, vAxis);
        SetPlayerMoveDirectionToCameraDirection(hAxis, vAxis);

        GroundCheck();
        if (isGrounded && calcVelocity.y < 0)
        {
            calcVelocity.y = 0;
            // animator.SetBool(GlobalDefine.FallingHash, false);
        }
        else if (!isGrounded && calcVelocity.y > 0)
        {
            //  animator.SetBool(GlobalDefine.FallingHash, true);
        }

        calcVelocity.y += gravity * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime);
        characterController.Move(calcVelocity * Time.deltaTime);
    }

    protected void PCMove()
    {
        if (gameplayTagSystem.HasTag(eTagType.Player_State_IgnoreInput))
            return;

        float hAxis = inputController.moveInputDirection.x;
        float vAxis = inputController.moveInputDirection.y;

        //�ִϸ��̼�
        controller.SetMoveDirection(hAxis, vAxis);

        //�̵�
        SetPlayerMoveDirectionToPlayerDirection(hAxis, vAxis);
        //�߷�
        GroundCheck();
        if (isGrounded && calcVelocity.y < 0)
        {
            calcVelocity.y = 0;
        }

        calcVelocity.y += gravity * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime);
        characterController.Move(calcVelocity * Time.deltaTime);
    }


    /// <summary>
    /// ī�޶� �������� �÷��̾� �̵� ���� ���ϱ�
    /// �̷��� �ϸ� ���縵�̳� ����ó�� ī�޶�� �÷��̾��� ������ �����ִ�.
    /// </summary>
    /// <param name="vAxis"></param>
    /// <param name="hAxis"></param>
    protected void SetPlayerMoveDirectionToCameraDirection(float hAxis, float vAxis)
    {
        if (cameraTransform == null)
        {
            Debug.LogError("Player => cameraTransform Null");
            return;
        }

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        moveDirection = forward * vAxis + right * hAxis;
        moveDirection *= attribute.GetCurValue(eAttributeType.Speed);
    }


    protected void SetPlayerMoveDirectionToPlayerDirection(float hAxis, float vAxis)
    {
        if (cameraTransform == null)
        {
            Debug.LogError("Player => cameraTransform Null");
            return;
        }

        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        moveDirection = forward * vAxis + right * hAxis;
        moveDirection *= attribute.GetCurValue(eAttributeType.Speed);
    }


    protected void RotateToCameraDirection()
    {
        bool isMoving = moveDirection.sqrMagnitude > moveThresholdSqr; // moveThresholdSqr = 0.01f ���� �̸� ����    
        if (isMoving)
        {
            RotateTowardsHorizontal(moveDirection);
        }
    }

    public float xx;

    protected void RotateToMouseDirection()
    {
        var result = Vector3.up * inputController.lookInputDirection.x * xx;

        transform.Rotate(result);        
    }


    protected void RotateTowardsHorizontal(Vector3 direction)
    {
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.001f) // ���� 0�� ������ ����
            return;

        Quaternion targetRotation = Quaternion.LookRotation(direction.normalized);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

}