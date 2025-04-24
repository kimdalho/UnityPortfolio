using UnityEngine;

public class Projectile : MonoBehaviour
{
    #region Base Stats
    [SerializeField] private float groundCheckDistance = 0.2f;
    [SerializeField] private float baseSpeed = 100f;    // ����ü �⺻ �ӵ�
    [SerializeField] private int penetrateCnt = 1;    // -1�� ������ ����
    [SerializeField] private float detectSize = 0.5f;   // Detect Object ũ��
    [SerializeField] private float gravity = 0f;
    [SerializeField] private float retentionTime = 3f;
    #endregion

    protected Character owner;
    protected LayerMask targetLayer;

    protected Vector3 moveDirection = Vector3.zero;
    private float accumulateGravityValue = 0f;
    protected eTagType abilityTag;


    protected virtual bool IsPoison => owner.gameplayTagSystem.HasTag(eTagType.Roostershead);
    protected int PenetrateCnt
    {
        get => owner.gameplayTagSystem.HasTag(eTagType.clowntorso) ? -1 : penetrateCnt;
        set => penetrateCnt = value;
    }

    /// <summary>
    /// ����ü �ʱ�ȭ
    /// </summary>
    /// <param name="owner">����ü ������ ��ü</param>
    /// <param name="dir">����ü ����</param>
    /// <param name="targetLayer">��� ���̾�</param>
    /// <param name="IsAtkSpeedAdd">����ü �̵� �ӵ��� ���� �ӵ� �ݿ� ����</param>
    public virtual void Initialized(Character owner, Vector3 dir, LayerMask targetLayer, eTagType abilityTag, bool IsAtkSpeedAdd = false)
    {
        this.owner = owner;
        this.targetLayer = targetLayer;
        this.abilityTag = abilityTag;

        //baseSpeed *= IsAtkSpeedAdd ? owner.attribute.attackSpeed : 1f;

        moveDirection = (dir + moveDirection).normalized;

        var _eulerAngle = Quaternion.LookRotation(moveDirection).eulerAngles;
        transform.rotation = Quaternion.Euler(_eulerAngle.x, _eulerAngle.y, 0f);

        Invoke("ReleaseProjectile", retentionTime);
    }

    /// <summary>
    /// Detect Object ����
    /// �������̵� �Ͽ� �ٸ� ������� ���� ����
    /// </summary>
    protected virtual void CreateDetectObject(float detectSize)
    {
        var _hits = SphereDetector.DetectObjectsInSphere(transform.position, detectSize, targetLayer);
        foreach (var _hit in _hits)
        {
            if (_hit.TryGetComponent<AttributeEntity>(out var _ae))
            {
                ApplyDamage(_ae);

                if (--PenetrateCnt == 0)
                {
                    if (gameObject == null) return;
                    ReleaseProjectile();
                }
            }
        }
    }

    protected virtual void ApplyDamage(AttributeEntity ae)
    {
        var _de = new DamageExecution();
        var _effect = new GameEffect(_de);
        _effect.Apply(owner, ae);

        (ae as Character)?.fxSystem?.ExecuteFX(abilityTag);

        if (IsPoison)
        {
            var _selfEffect = new GameEffectSelf();
            _selfEffect.effect.CurHart -= 1;
            (ae as Character)?.OnHit(_selfEffect);
        }

        // óġ �̺�Ʈ
        if (ae.attribute.CurHart <= 0)
        {
            owner.onKill?.Invoke();
        }
    }

    protected virtual void Move()
    {
        transform.Translate(moveDirection * baseSpeed * Time.deltaTime, Space.World);

        
    }

    private void ApplyGravity()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(90f, transform.eulerAngles.y, 0f), Time.deltaTime);

        accumulateGravityValue += gravity * Time.deltaTime;
        transform.Translate(new Vector3(0f, accumulateGravityValue, 0f) * Time.deltaTime, Space.World);
    }

    protected virtual void ReleaseProjectile()
    {
        Destroy(gameObject);
    }

    private void CheckGrounded()
    {
        var _hit = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, LayerMask.GetMask("Default"));
        if (_hit && gameObject != null) ReleaseProjectile();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
    }

    protected virtual void Update()
    {
        CreateDetectObject(detectSize);
        Move();

        if (gravity >= 0) return; // �߷� ���� X
        ApplyGravity();
        CheckGrounded();
    }
}
