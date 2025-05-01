using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : IState
{
    public GameOverState(StateMachine stateMachine)
    {
        FSM = stateMachine;
    }
    
    public void EnterState()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateState()
    {
        throw new System.NotImplementedException();
    }

    public void ExitState()
    {
        throw new System.NotImplementedException();
    }

    public StateMachine FSM { get; }
}
