using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor.Purchasing;
using UnityEngine;

public partial class Player : Character
{
    float hAxis;
    float vAxis;

    private InputController inputController;
    public float rotationSpeed = 10f;
    public Transform cameraTransform;
    private Vector3 moveDirection;

    [SerializeField]
    private ModelController modelController;
    private Door door;

    AbilitySystem abilitySystem;

    private void Start()
    {
        LoadData();
        abilitySystem = GameManager.instance.abilitySystem;
        if (abilitySystem == null)
        {
            Debug.LogError("AbilitySystem�� ã�� �� �����ϴ�!");
            return;
        }

        gameObject.tag = "Player";

        inputController = InputController.Instance;
        if (inputController == null)
        {
            Debug.LogError("InputController�� ã�� �� �����ϴ�!");
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
            Debug.LogError("AbilitySystem�� �������� �ʾ� AbilitySkillAttackEnd() ���� �Ұ�!");
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
        animator.SetBool(fallingHash, !isGrounded);

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

    private void LoadData()
    {
        if (UserData.Instance == null)
        {
            attribute.speed = 7;
            return;
        }
            

        attribute = UserData.Instance.saveData.attribute;
        modelController.DisableAllParts();
        modelController.m_heads[UserData.Instance.saveData.headIndex].gameObject.SetActive(true);
        modelController.m_bodys[UserData.Instance.saveData.bodyIndex].gameObject.SetActive(true);
        modelController.m_weapons[UserData.Instance.saveData.weaponIndex].gameObject.SetActive(true);
    }

  
}

