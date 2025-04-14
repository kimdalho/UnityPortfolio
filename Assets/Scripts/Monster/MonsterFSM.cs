using System;
using System.Collections.Generic;
using UnityEngine;
public class MonsterFSM : MonoBehaviour
{
    private Monster monster;
    private MonsterState currentState = MonsterState.Idle;
    private List<Action> actions = new List<Action>();

    // Init
    public void Initialized(Monster monster, params Action[] actions)
    {
        this.monster = monster;
        for (int i = 0; i < actions.Length; i++)
        {
            this.actions.Add(actions[i]);
        }
    }

    private void ChangeState(MonsterState newState)
    {
        // 같은 상태로 변환에 대한 연산 허용 X
        if (currentState.Equals(newState)) return;

        currentState = newState;
    }

    private void UpdateState()
    {
        var _index = (int)currentState;
        actions[_index].Invoke();
    }

    private void DecideState()
    {
        if (monster.attribute.CurHart <= 0)
        {
            ChangeState(MonsterState.Dead);
            return;
        }

        if (monster.IsHit)
        {
            ChangeState(MonsterState.Hit);
            return;
        }

        if (monster.CurBullet.Equals(0) && !monster.IsAtk)
        {
            ChangeState(MonsterState.Reload);
            return;
        }

        if (monster.chaseTarget != null)
        {
            if ((Vector3.Distance(transform.position, monster.chaseTarget.transform.position) <= monster.attackRange && !monster.IsAtkCool) || monster.IsAtk)
            {
                ChangeState(MonsterState.Attack);
                return;
            }

            if (Vector3.Distance(transform.position, monster.chaseTarget.transform.position) > monster.attackRange)
            {
                ChangeState(MonsterState.Chase);
                return;
            }
        }

        if (monster.patrolTargetPos != default(Vector3) && monster.chaseTarget == null)
        {
            ChangeState(MonsterState.Patrol);
            return;
        }

        ChangeState(MonsterState.Idle);
    }

    private void Update()
    {
        DecideState();
        UpdateState();
    }
}
