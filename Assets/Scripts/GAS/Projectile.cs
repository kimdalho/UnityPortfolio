using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class Projectile : MonoBehaviour
{
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
        float attackSpeed = owner.attribute.GetCurValue(eAttributeType.AttackSpeed);

        moveDirection = dir;

        var _eulerAngle = Quaternion.LookRotation(moveDirection).eulerAngles;
        transform.rotation = Quaternion.Euler(_eulerAngle.x, _eulerAngle.y, 0f);
    } 

    protected virtual void Move()
    {
        transform.Translate(moveDirection * 1 * Time.deltaTime, Space.World);        
    }

    protected virtual void ReleaseProjectile()
    {
        Destroy(gameObject);
    }


    protected virtual void Update()
    {
        Move();
    }
}
