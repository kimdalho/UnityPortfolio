using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IState
{
    public bool IsState(Monster monster); // ���°� Ȱ��ȭ �Ǿ����� Ȯ��
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
        // ���� ���·� ��ȯ�� ���� ���� ��� X
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
        Debug.Log("�� ���� ����" + currentState);
    }

    public Monster GetMon()
    {
        return monster; 
    }
}
