using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IState
{
    public bool IsState(Monster monster); // 상태가 활성화 되었는지 확인
    public void Enter(Monster monster);
    public void Action(Monster monster);
    public void Exit(Monster monster);
}

public class MonsterFSM : MonoBehaviour
{
    private Monster monster;
    private IState currentState;
    private List<IState> states;

    // Init
    public void Initialized(Monster monster, params IState[] states)
    {
        this.monster = monster;
        this.states = states.ToList();

        currentState = states[states.Length - 1];
    }

    private void ChangeState(IState newState)
    {
        // 같은 상태로 변환에 대한 연산 허용 X
        if (currentState.Equals(newState)) return;

        currentState.Exit(monster);
        newState.Enter(monster);
        currentState = newState;
    }

    private void UpdateState()
    {
        currentState.Action(monster);
    }

    private void DecideState()
    {
        foreach (var _state in states)
        {
            if (_state.IsState(monster))
            {
                ChangeState(_state);
                return;
            }
        }
    }

    private void Update()
    {      
        DecideState();
        UpdateState();
        Debug.Log("내 현재 상태" + currentState);
    }

    public Monster GetMon()
    {
        return monster; 
    }
}
