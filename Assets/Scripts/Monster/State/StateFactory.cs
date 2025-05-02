using System.Collections.Generic;
using UnityEngine;

public enum MonsterState
{
    Idle,
    Patrol,
    Chase,
    Attack,
    Reload,
    Hit,
    Dead,
}

public class StateFactory
{
    private static Dictionary<MonsterState, IState> stateCashes = new Dictionary<MonsterState, IState>();

    public static IState GetState(MonsterState state)
    {
        if (stateCashes.TryGetValue(state, out var cachedState))
        {
            return cachedState;
        }
        IState newState = state switch
        {
            MonsterState.Idle => new IdleState(),
            MonsterState.Patrol => new PatrolState(),
            MonsterState.Chase => new ChaseState(),
            MonsterState.Attack => new AttackState(),
            MonsterState.Reload => new ReLoadState(),
            MonsterState.Hit => new HitState(),
            MonsterState.Dead => new DeadState(),
            _ => null
        };
        if (newState != null)
        {
            stateCashes[state] = newState;
        }
        return newState;
    }

}
