using System;
using System.Collections.Generic;
using UnityEditor.Purchasing;
using UnityEngine;

public partial class Player : Character, IPlayerserveice
{
    private InputController inputController;

    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public Transform cameraTransform;
    private CharacterController characterController;
    private Vector3 moveDirection;

    float hAxis;
    float vAxis;
    Vector3 moveVec;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private PlayerInventory inventory;
    private Door door;
    [SerializeField] private Animator animator;

    AbilitySystem abilitySystem;

    private void Start()
    {
        abilitySystem = GameObject.Find("AbilitySystem")?.GetComponent<AbilitySystem>();
        if (abilitySystem == null)
        {
            Debug.LogError("AbilitySystem을 찾을 수 없습니다!");
            return;
        }

        gameObject.tag = "Player";
        rb = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();

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
        Move();
        RotateToCameraDirection();
        DropItemUpdate();
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
        if (door != null)
            door.Open();
    }

    private void AbilitySkillX()
    {
        abilitySystem.ActivateAbility("FireBall", this);
    }

    private void AbilitySkillAttack()
    {
        abilitySystem.ActivateAbility("Attack", this);
    }

    private void AbilitySkillAttackEnd()
    {
        if (abilitySystem == null)
        {
            Debug.LogError("AbilitySystem이 존재하지 않아 AbilitySkillAttackEnd() 실행 불가!");
            return;
        }

        abilitySystem.DeactivateAbility("Attack");
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
        if (other.CompareTag("Door"))
        {
            door = other.GetComponent<Door>();
        }
        else if (other.TryGetComponent(out DroppedItem item))
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

    public List<ItemData> itemlist = new List<ItemData>();
}