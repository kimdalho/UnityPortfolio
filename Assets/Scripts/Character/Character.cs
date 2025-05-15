using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEngine.UI.GridLayoutGroup;

public interface ILockOnTarget
{
    public Transform GetLockOnTransform();
    public bool GetDead();
}

public interface IGameAbilityCharacterService
{
    public AbilitySystem GetAbilitySystem();

    public GameplayTagSystem GetGameplayTagSystem();
}


public interface IWeaponService
{
    public Transform GetWeaponMuzzle();
}


public class Character : AttributeEntity , ILockOnTarget , IGameAbilityCharacterService , IWeaponService
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
    public bool isDead;
   
    [SerializeField] protected ModelController controller;

    public FXSystem fxSystem;

    [SerializeField]
    protected AbilitySystem abilitySystem;

    //그라운드
    protected RaycastHit groundhit;

    protected Transform armTransform;

    public WeaponController currentWeaponEffect;

    protected virtual void Awake()
    {
        abilitySystem = GetComponentInChildren<AbilitySystem>();
        currentWeaponEffect = null;
    }

    //제거 대상자
    public Animator GetAnimator()
    {
        return controller.animator;
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
        return isDead;
    }

    public AbilitySystem GetAbilitySystem()
    {
        return abilitySystem;
    }

    public GameplayTagSystem GetGameplayTagSystem()
    {
        return gameplayTagSystem;
    }

    public virtual Transform GetWeaponMuzzle()
    {
        if (armTransform == null)
        {
            transform.GetChild(0).GetChild(transform.GetChild(0).childCount - 1);
            for (int i = 0; i < 8; i++)
                armTransform = armTransform.GetChild(0);
        }
        return armTransform;
    }

   

    public virtual void SetWeaponEffect(WeaponController NewWeapon)
    {
        currentWeaponEffect = NewWeapon;
        armTransform = currentWeaponEffect.bulletStartPos;
    }

}
