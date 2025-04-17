using UnityEngine;

public class Projectile : MonoBehaviour
{
    #region Base Stats
    [SerializeField] private float groundCheckDistance = 0.2f;
    [SerializeField] private float baseSpeed = 100f;    // 투사체 기본 속도
    [SerializeField] private float PenetrateCnt = 1;    // -1은 무제한 관통
    [SerializeField] private float detectSize = 0.5f;   // Detect Object 크기
    [SerializeField] private float gravity = 0f;
    [SerializeField] private float retentionTime = 3f;
    #endregion

    [SerializeField] private Rigidbody rig;
    protected Character owner;
    protected LayerMask targetLayer;

    private Vector3 moveDirection;
    private float accumulateGravityValue = 0f;
    protected eTagType abilityTag;

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// 투사체 초기화
    /// </summary>
    /// <param name="owner">투사체 생성한 객체</param>
    /// <param name="dir">투사체 방향</param>
    /// <param name="targetLayer">대상 레이어</param>
    /// <param name="IsAtkSpeedAdd">투사체 이동 속도에 공격 속도 반영 여부</param>
    public void Initialized(Character owner, Vector3 dir, LayerMask targetLayer, eTagType abilityTag, bool IsAtkSpeedAdd = false)
    {
        this.owner = owner;
        this.targetLayer = targetLayer;
        this.abilityTag = abilityTag;

        baseSpeed *= IsAtkSpeedAdd ? owner.attribute.attackSpeed : 1f;

        moveDirection = dir;

        var _eulerAngle = Quaternion.LookRotation(moveDirection).eulerAngles;
        transform.rotation = Quaternion.Euler(_eulerAngle.x, _eulerAngle.y, 0f);

        Invoke("ReleaseProjectile", retentionTime);
    }

    /// <summary>
    /// Detect Object 생성
    /// 오버라이딩 하여 다른 방식으로 생성 가능
    /// </summary>
    protected virtual void CreateDetectObject(float detectSize)
    {
        var _hits = SphereDetector.DetectObjectsInSphere(transform.position, detectSize, targetLayer);
        foreach (var _hit in _hits)
        {
            if (_hit.TryGetComponent<AttributeEntity>(out var _ae))
            {
                var _effect = new GameEffect(new DamageExecution());
                _effect.Apply(owner, _ae);
                (_ae as Character)?.fxSystem?.ExecuteFX(abilityTag);

                if (--PenetrateCnt > 0) return;

                if (gameObject == null) return;
                ReleaseProjectile();
            }
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

        if (gravity >= 0) return; // 중력 적용 X
        ApplyGravity();
        CheckGrounded();
    }
}
