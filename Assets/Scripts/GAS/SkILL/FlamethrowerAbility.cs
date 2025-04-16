using UnityEngine;
using System.Collections;

public class FlamethrowerAbility : GameAbility
{
    // 임시 확인
    [SerializeField] private ParticleSystem particle;
    private float applyPerSecond = 5f;

    protected override IEnumerator ExecuteAbility()
    {
        FXFactory.Instance.GetFX("FlameThrower", owner.transform.position, Quaternion.LookRotation(owner.transform.forward));

        owner.GetAnimator().SetBool("FlameThrower", true);

        var _center = owner.transform.position + owner.transform.forward * 1f;

        var _applyCount = Duration * applyPerSecond;
        var _waitTime = Duration / applyPerSecond;
        for (int i = 0; i < _applyCount; i++)
        {
            var _hits = SphereDetector.DetectObjectsInSphere(_center, 1f, targetMask);

            foreach (var _hit in _hits)
            {
                if (_hit.TryGetComponent<AttributeEntity>(out var _ae))
                {
                    var _effect = new GameEffect(new DamageExecution());
                    _effect.Apply(owner, _ae);
                    // FX 적용
                }
            }

            yield return new WaitForSeconds(_waitTime);
        }

        owner.GetAnimator().SetBool("FlameThrower", false);
        isCreate = false;

        var _delay = Duration / Mathf.Max(owner.attribute.attackSpeed, 0.01f);
        yield return new WaitForSeconds(_delay);
        EndAbility();
    }

    private bool isCreate = false;

    public new void ActivateAbility(Character owner)
    {
        if (!isCreate)
        {
            this.owner = owner;
            FXFactory.Instance.GetFX("FlameThrower", owner.transform.position, Quaternion.LookRotation(owner.transform.forward));
            isCreate = true;
        }

        if (owner is Player)
        {

        }
        else
        {
            // Monster일 경우엔 기존 로직 그대로 실행
            base.ActivateAbility(owner);
        }
    }
}
