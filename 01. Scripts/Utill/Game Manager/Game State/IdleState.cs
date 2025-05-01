using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    public IdleState(StateMachine stateMachine)
    {
        FSM = stateMachine;
        
        EventBus.RegisterEvent(GameEventType.ENCOUNTER, () => FSM.ChangeState<EncounterState>());
    }
    
    public void EnterState()
    {
        EventBus.Publish(GameEventType.IDLE);
    }

    public void UpdateState()
    {

    }

    public void ExitState()
    {

    }

    public StateMachine FSM { get; }
}
