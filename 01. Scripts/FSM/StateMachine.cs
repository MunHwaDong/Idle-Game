using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine
{
    public void Constructor(Dictionary<Type, IState> states, Type defaultState)
    {
        _states = states;

        if (_states.TryGetValue(defaultState, out IState state))
        {
            _defaultState = state;
            _currentState = state;
        }
        else
            throw new KeyNotFoundException("default state를 찾을 수 없습니다.");
    }

    public void InitMachine(Blackboard blackboard)
    {
        Blackboard = blackboard;
        
        _currentState?.EnterState();
    }
    
    public void ChangeState<T>()
    {
        _currentState?.ExitState();
        
        _currentState = _states[typeof(T)];
        
        _currentState?.EnterState();
    }
    
    public void Run()
    {
        _currentState?.UpdateState();
    }

    public void InitState()
    {
        _currentState = _defaultState;
    }

    private IState _currentState;
    private IState _defaultState;

    private IDictionary<Type, IState> _states;
    
    public Blackboard Blackboard { get; private set; }
}
