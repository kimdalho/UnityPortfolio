using UnityEngine;

public class BlackHood : Monster
{
    protected override void ExecuteAttack()
    {
        // Create Dectect Object
        var _hits = SphereDetector.DetectObjectsInSphere(transform.position + transform.forward * attackRange, 1, LayerMask.GetMask("Player"));
        if (_hits == null || _hits.Length.Equals(0)) return;

        if (_hits[0].TryGetComponent<AttributeEntity>(out var _obj))
        {
            var _effect = new GameEffect(new DamageExecution());
            _effect.Apply(this, _obj);
        }
    }
}