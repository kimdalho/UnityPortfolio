using UnityEngine;

public interface ILockOnService
{
    public Transform GetLockOnTransform();
    public bool GetDead();
}

public interface IGameAbilityCharacterService
{
    public AbilitySystem GetAbilitySystem();

    public GameplayTagSystem GetGameplayTagSystem();
}


public class Character : AttributeEntity , ILockOnService , IGameAbilityCharacterService
{

    //저항력
    public Vector3 calcVelocity;


    //플레이어가 획득한 상태효과
    protected GameplayTagSystem gameplayTagSystem = new GameplayTagSystem();

    [SerializeField]
    protected CharacterController characterController;

    public float jumpHeight = 2f;
    public float gravity = -9.81f;

    protected bool isGrounded = false;
    public LayerMask groundLayerMask;
    public float groundCheckDistance = 0.1f;

    [SerializeField] protected Animator animator;
    [SerializeField] protected ModelController controller;

    public FXSystem fxSystem;

    [SerializeField]
    protected AbilitySystem abilitySystem;

    //그라운드
    protected RaycastHit groundhit;

    private void Awake()
    {
        abilitySystem = GetComponentInChildren<AbilitySystem>();

    }


    public Animator GetAnimator()
    {
        return animator;
    }

    public ModelController GetModelController()
    {
        return controller;
    }


    public virtual Transform GetLockOnTransform()
    {
        return gameObject.transform;
    }

    
    protected void GroundCheck()
    {
        Debug.DrawRay(transform.position, transform.up * -1 * groundCheckDistance, Color.blue, 0.3f);
        if (Physics.Raycast(transform.position, transform.up * -1, out groundhit, groundCheckDistance, groundLayerMask))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    public virtual bool GetDead()
    {
        return false;
    }

    public AbilitySystem GetAbilitySystem()
    {
        return abilitySystem;
    }

    public GameplayTagSystem GetGameplayTagSystem()
    {
        return gameplayTagSystem;
    }
}
