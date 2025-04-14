using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    private Rigidbody rig;
    private Character owner;

    [SerializeField] private float projectileSpeed = 100f;
    [SerializeField] private float PenetrateCnt = 1; // -1은 무제한 관통
    [SerializeField] private float detectSize = 0.5f;

    private LayerMask targetLayer;

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    public void Initialized(Character owner, Vector3 dir, LayerMask targetLayer)
    {
        this.owner = owner;
        this.targetLayer = targetLayer;

        rig.linearVelocity = dir.normalized * projectileSpeed * Time.fixedDeltaTime;
    }

    protected virtual void CreateDetectObject()
    {
        var _hits = SphereDetector.DetectObjectsInSphere(rig.position, detectSize, targetLayer);
        foreach (var _hit in _hits)
        {
            if (_hit.TryGetComponent<AttributeEntity>(out var _ae))
            {
                var _effect = new GameEffect(new DamageExecution());
                //_effect.Apply(owner, _ae);

                if (--PenetrateCnt > 0) return;

                ReleaseProjectile();
            }
        }
    }

    protected virtual void ReleaseProjectile()
    {
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        CreateDetectObject();
    }
}
