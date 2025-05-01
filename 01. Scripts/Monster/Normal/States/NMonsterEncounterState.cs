using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class NMonsterEncounterState : IState
{
    public NMonsterEncounterState(StateMachine stateMachine)
    {
        FSM = stateMachine;
    }
    
    public void EnterState()
    {
        _animator ??= FSM.Blackboard.ReadData<Animator>();
    }

    public void UpdateState()
    {
        //TODO: 몬스터 공격 로직 구현
    }

    public async void ExitState()
    {
        
    }

    public StateMachine FSM { get; }
    private Animator _animator;
}
