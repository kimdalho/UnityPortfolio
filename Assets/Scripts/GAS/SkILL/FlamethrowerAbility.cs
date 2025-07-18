using UnityEngine;
using System.Collections;
using static UnityEngine.UI.GridLayoutGroup;

public class FlamethrowerAbility : GameAbility
{
    // 임시 확인
    private float applyPerSecond = 5f;

    protected override IEnumerator ExecuteAbility()
    {
        // 임시 FX 위치
        var _fxPos = owner.transform.position + Vector3.up * 0.6f + owner.transform.forward * 0.7f;
        // 화염 방사기 FX 생성
        var _particle = FXFactory.Instance.GetFX("FlameThrower", _fxPos, Quaternion.LookRotation(owner.transform.forward));

        owner.GetAnimator().SetBool("LoopAttack", true);
        var takeDamage = owner.attribute.GetCurValue(eAttributeType.Attack);
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
                    GameEffect effect = new GameEffect(eModifier.Add);
                    effect.AddModifier(eAttributeType.Health, -takeDamage);
                    _ae.ApplyEffect(effect);
                    // FX 적용
                }
            }

            yield return new WaitForSeconds(_waitTime);
        }

        // 화염 방사기 FX 자연스럽게 삭제
        //_particle.GetComponent<ParticleSystem>().Stop(true, ParticleSystemStopBehavior.StopEmitting);
        owner.GetAnimator().SetBool("LoopAttack", false);

        if (owner is Monster) (owner as Monster).IsAtk = false;

        var _delay = Duration / Mathf.Max(owner.attribute.GetCurValue(eAttributeType.AttackSpeed), 0.01f);
        yield return new WaitForSeconds(_delay);
        EndAbility();
    }
}
