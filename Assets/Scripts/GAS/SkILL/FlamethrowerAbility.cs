using UnityEngine;
using System.Collections;

public class FlamethrowerAbility : GameAbility
{
    // �ӽ� Ȯ��
    private float applyPerSecond = 5f;

    protected override IEnumerator ExecuteAbility()
    {
        // ȭ�� ���� FX ����
        var _particle = FXFactory.Instance.GetFX("FlameThrower", owner.transform.position + Vector3.up * 0.5f, Quaternion.LookRotation(owner.transform.forward));

        owner.GetAnimator().SetBool("LoopAttack", true);

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
                    // FX ����
                }
            }

            yield return new WaitForSeconds(_waitTime);
        }

        // ȭ�� ���� FX �ڿ������� ����
        _particle.GetComponent<ParticleSystem>().Stop(true, ParticleSystemStopBehavior.StopEmitting);
        owner.GetAnimator().SetBool("LoopAttack", false);

        if (owner is Monster)
        {
            (owner as Monster).IsAtk = false;
        }

        var _delay = Duration / Mathf.Max(owner.attribute.attackSpeed, 0.01f);
        yield return new WaitForSeconds(_delay);
        EndAbility();
    }
}
