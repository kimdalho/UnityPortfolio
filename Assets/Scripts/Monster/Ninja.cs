using UnityEngine;

public class Ninja : Samurai
{
    [SerializeField] private Transform armTrans;

    protected override void Initialized()
    {

        // Hit 상태로 변경될 수 있도록 구독
        OnHit += TakeDamage;

        // 상태 초기화
        var _idle = StateFactory.GetState(MonsterState.Idle);
        var _attack = StateFactory.GetState(MonsterState.Attack);
        var _reLoad = StateFactory.GetState(MonsterState.Reload);
        var _hit = StateFactory.GetState(MonsterState.Hit);
        var _dead = StateFactory.GetState(MonsterState.Dead);

        GetComponent<MonsterFSM>().Initialized(this, _dead, _hit, _reLoad, _attack, _idle);
    }

    protected override void ExecuteAttack()
    {
        abilitySystem.ActivateAbility(eTagType.Attack, this);
    }
}