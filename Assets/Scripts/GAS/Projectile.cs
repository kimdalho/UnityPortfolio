using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody rig;
    protected Character owner;
    protected LayerMask targetLayer;

    private Vector3 moveDirection;
    private float accumulateGravityValue = 0f;
    protected eTagType abilityTag;
    [SerializeField] private float projectileBaseSpeed;

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
        Destroy(gameObject, 2f);
    } 

    protected virtual void Move()
    {
        transform.Translate(moveDirection * projectileBaseSpeed * Time.deltaTime, Space.World);        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Bullet 닿음");
    }


    protected virtual void Update()
    {
        Move();
        float sphereRadius = 0.3f; // 구체 반지름 조정 가능
        float takeDamage = owner.attribute.GetCurValue(eAttributeType.Attack);

        Collider[] results = SphereDetector.DetectObjectsInSphere(transform.position, sphereRadius, targetLayer);
        foreach (var col in results)
        {
            AttributeEntity ae = col.GetComponent<AttributeEntity>();
            if (ae != null)
            {
                var character = ae as Character;

                if (character != null && character != owner)
                {
                    
                    if (character.fxSystem != null)
                    {
                        GameEffect effect = new GameEffect(eModifier.Add);
                        effect.AddModifier(eAttributeType.Health, -takeDamage);
                        ae.ApplyEffect(effect);
                    }
                    else
                    {
                        Debug.LogWarning("맞은 대상 FX가 없다");
                    }

                    Destroy(gameObject); // 투사체 제거
                    break; // 하나만 맞고 사라지게
                }
            }
        }
    }
}
