using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerEncounterState : IState
{
    public PlayerEncounterState(StateMachine stateMachine)
    {
        FSM = stateMachine;
        
        EventBus.RegisterEvent(GameEventType.IDLE, () => FSM.ChangeState<PlayerIdleState>());
    }
    
    public void EnterState()
    {
        _animController ??= FSM.Blackboard.ReadData<SPUM_Prefabs>();
        _playerWeapon ??= FSM.Blackboard.ReadData<Weapon>();
        
        _animController.PlayAnimation("2_Attack_Normal");
        _playerWeapon.OnDamageField();
    }

    public void UpdateState()
    {
        FSM.ChangeState<PlayerEncounterState>();
    }

    public void ExitState()
    {

    }

    public StateMachine FSM { get; }
    
    private SPUM_Prefabs _animController;
    private Weapon _playerWeapon;
}
