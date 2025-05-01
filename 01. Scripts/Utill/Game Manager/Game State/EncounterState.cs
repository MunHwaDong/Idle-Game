using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterState : IState
{
    public EncounterState(StateMachine stateMachine)
    {
        FSM = stateMachine;
    }
    
    public void EnterState()
    {
        
    }

    public void UpdateState()
    {
        if (GameManager.Instance.monsterDeathFlag is true)
        {
            //TODO: Gold 반영하기
            FSM.ChangeState<IdleState>();
        }
    }

    public void ExitState()
    {
        GameManager.Instance.monsterDeathFlag = false;
    }

    public StateMachine FSM { get; }
}
