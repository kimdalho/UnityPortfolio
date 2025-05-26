using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem.XInput;


public class RigModelController : ModelController
{

    [SerializeField] private Transform headTarget;
    [SerializeField] private float verticalMoveSpeed = 0.05f;
    [SerializeField] private float minY = -0.1f;
    [SerializeField] private float maxY = 0.1f;
    [SerializeField] private Transform basePos;
    private float currentOffsetY = 0f;

    //�����ٵ� Ÿ�Կ��� ���⸦ ���������ʴ�public Dictionary<eEuipmentType, List<GameObject>> partsByType = new Dictionary<eEuipmentType, List<GameObject>>();��.    
    protected override  void InitializeParts()
    {
        base.InitializeParts();
    }

    protected override void Awake()
    {
        base.Awake();
        InitializeParts();
        dicWeapons = new Dictionary<eWeaponType, WeaponController>();
        foreach (var weapon in m_weapons)
        {
            dicWeapons.Add(weapon.eWeaponType, weapon);
        }
    }
    public void OnJumpStartAnimationComplete() // Animation event at end of JumpStart clip
    {
        if (SoundManager.instance != null)
            SoundManager.instance.PlayEffect(eEffectType.Jump);
        SetState(AnimState.InAir);
    }

    public void OnLandAnimationComplete() // Animation event at end of Land clip
    {
        SetState(AnimState.Idle);
    }

    public void HeadController(float inputY)
    {

        // �Է°� ����
        currentOffsetY += inputY * verticalMoveSpeed;
        currentOffsetY = Mathf.Clamp(currentOffsetY, minY, maxY);

        // ���� ��ġ���� Y�����θ� ������ ����
        Vector3 basePosition = basePos.transform.position;
        headTarget.position = basePosition + Vector3.up * currentOffsetY;
    }

}
