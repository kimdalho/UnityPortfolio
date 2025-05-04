using UnityEngine;

public class Ninja : Samurai
{
    [SerializeField] private Transform armTrans;

    protected override void Initialized()
    {

        // Hit ���·� ����� �� �ֵ��� ����
        OnHit += TakeDamage;

        // ���� �ʱ�ȭ
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