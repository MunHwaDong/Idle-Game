using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMonsterIdleState : IState
{
    public NMonsterIdleState(StateMachine stateMachine)
    {
        FSM = stateMachine;
    }
    
    public void EnterState()
    {
        _animator ??= FSM.Blackboard.ReadData<Animator>();
        _transform ??= FSM.Blackboard.ReadData<Transform>();
        
        EventBus.RegisterEvent(GameEventType.ENCOUNTER, () => FSM.ChangeState<NMonsterEncounterState>());
    }

    public void UpdateState()
    {
        _animator.Play("Run");
        
        _transform.position += Vector3.left * (Time.deltaTime * 4f);
    }

    public void ExitState()
    {
        _animator.CrossFade("Idle", 0.1f);
    }

    public StateMachine FSM { get; }
    private Animator _animator;
    private Transform _transform;
}
