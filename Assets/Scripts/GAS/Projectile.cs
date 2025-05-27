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
    /// ����ü �ʱ�ȭ
    /// </summary>
    /// <param name="owner">����ü ������ ��ü</param>
    /// <param name="dir">����ü ����</param>
    /// <param name="targetLayer">��� ���̾�</param>
    /// <param name="IsAtkSpeedAdd">����ü �̵� �ӵ��� ���� �ӵ� �ݿ� ����</param>
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
        Debug.Log("Bullet ����");
    }


    protected virtual void Update()
    {
        Move();
        float sphereRadius = 0.3f; // ��ü ������ ���� ����
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
                        Debug.LogWarning("���� ��� FX�� ����");
                    }

                    Destroy(gameObject); // ����ü ����
                    break; // �ϳ��� �°� �������
                }
            }
        }
    }
}
