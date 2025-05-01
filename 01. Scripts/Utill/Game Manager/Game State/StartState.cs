using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState : IState
{
    public StartState(StateMachine stateMachine)
    {
        FSM = stateMachine;
    }
    
    public void EnterState()
    {
        GameManager.Instance.InitPlayerData();
        
        EventBus.Publish(GameEventType.GAME_START);
        
        FSM.ChangeState<IdleState>();
    }

    public void UpdateState()
    {

    }

    public void ExitState()
    {

    }

    public StateMachine FSM { get; }
}
