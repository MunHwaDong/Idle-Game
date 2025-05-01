using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IState
{
    public PlayerIdleState(StateMachine stateMachine)
    {
        FSM = stateMachine;
        
        EventBus.RegisterEvent(GameEventType.ENCOUNTER, () => FSM.ChangeState<PlayerEncounterState>());
    }
    
    public void EnterState()
    {
        _animController ??= FSM.Blackboard.ReadData<SPUM_Prefabs>();
        
        _animController.PlayAnimation("1_Run");
    }

    public void UpdateState()
    {

    }

    public void ExitState()
    {

    }

    public StateMachine FSM { get; }

    private SPUM_Prefabs _animController;
}
