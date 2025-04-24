using UnityEngine;

public class BazookaMissile : Projectile
{
    [SerializeField] private float explosionRange = 3f;

    public override void Initialized(Character owner, Vector3 dir, LayerMask targetLayer, eTagType abilityTag, bool IsAtkSpeedAdd = false)
    {
        moveDirection += Vector3.up * 0.3f;
        base.Initialized(owner, dir, targetLayer, abilityTag, IsAtkSpeedAdd);
    }

    protected override void CreateDetectObject(float detectSize)
    {
        var _hits = SphereDetector.DetectObjectsInSphere(transform.position, detectSize, targetLayer);
        if (_hits == null || _hits.Length.Equals(0)) return;

        ReleaseProjectile();
    }

    protected override void ReleaseProjectile()
    {
        FXFactory.Instance.GetFX("Explosion", transform.position, Quaternion.identity);

        var _hits = SphereDetector.DetectObjectsInSphere(transform.position, explosionRange, targetLayer);
        foreach (var _hit in _hits)
        {
            if (_hit.TryGetComponent<AttributeEntity>(out var _ae))
            {
                ApplyDamage(_ae);
                //var _effect = new GameEffect(new DamageExecution());
                //_effect.Apply(owner, _ae);
                //(_ae as Character)?.fxSystem?.ExecuteFX(abilityTag);
            }
        }

        base.ReleaseProjectile();
    }
}
