using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[
    RequireComponent(typeof(SPUM_Prefabs))
]
public class PlayerController : MonoBehaviour
{
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _playerStateMachine ??= Factory.CreateItems<PlayerStateFactory>() as StateMachine;

        if (TryGetComponent(out _animController) is false)
        {
            throw new Exception("SPUM_Prefabs을 찾을 수 없습니다.");
        }

        Blackboard.WriteData(typeof(SPUM_Prefabs), _animController);
        Blackboard.WriteData(typeof(Weapon), GetComponentInChildren<Weapon>());

        if (_playerStateMachine is not null)
            _playerStateMachine.InitMachine(Blackboard);
        else
            throw new Exception("State Machine이 제대로 생성되지 않았습니다.");
    }

    private void Update()
    {
        if (_playerStateMachine is null)
            Init();
        
        _playerStateMachine.Run();
    }

    private SPUM_Prefabs _animController;

    private StateMachine _playerStateMachine;
    public Blackboard Blackboard { get; } = new Blackboard();
}
