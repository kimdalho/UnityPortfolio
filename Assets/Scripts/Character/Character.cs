using System;
using UnityEngine;

public class Character : AttributeEntity
{

    //저항력
    public Vector3 calcVelocity;
    protected readonly int moveHash = Animator.StringToHash("Move");
    protected readonly int fallingHash = Animator.StringToHash("Falling");

    //플레이어가 획득한 상태효과
    public GameplayTagSystem gameplayTagSystem = new GameplayTagSystem();

    [SerializeField]
    protected CharacterController characterController;

    public float jumpHeight = 2f;
    public float gravity = -9.81f;

    protected bool isGrounded = false;
    public LayerMask groundLayerMask;
    public float groundCheckDistance = 0.3f;

    [SerializeField] protected Animator animator;
    [SerializeField] protected ModelController controller;

    public FXSystem fxSystem;
    public Transform curWeaponTrans;

    public Animator GetAnimator()
    {
        return animator;
    }

    public ModelController GetModelController()
    {
        return controller;
    }

    public void OnHit(IGameEffect effect)
    {
        effect.ApplyGameplayEffectToSelf(this);
    }

}
