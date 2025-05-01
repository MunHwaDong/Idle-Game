using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[
    RequireComponent(typeof(Animator))
]
public class NormalMonster : IMonster
{
    protected override void Init()
    {
        base.Init();

        if (_monsterStateMachine is null)
        {
            _monsterStateMachine = Factory.CreateItems<NormalMonsterStateFactory>() as StateMachine;
        
            if (TryGetComponent(out animController) is false)
            {
                throw new Exception("Animator를 찾을 수 없습니다.");
            }
        
            Blackboard.WriteData(typeof(Animator), animController);
            Blackboard.WriteData(typeof(Transform), transform);
            Blackboard.WriteData(typeof(int), HP);

            if (_monsterStateMachine is not null)
                _monsterStateMachine.InitMachine(Blackboard);
            else
                throw new Exception("State Machine이 제대로 생성되지 않았습니다.");
        }
        else
        {
            _monsterStateMachine.InitState();
        }
    }
    
    public override void OnEvent(EventMessage msg)
    {
        if (msg.GetParameter<float>().Count > 1 || msg.GetParameter<bool>().Count > 1)
            throw new Exception("데미지 값이 1개 이상 들어있습니다.");

        float damage = msg.GetParameter<float>()[0];
        bool isCritical = msg.GetParameter<bool>()[0];

        TakenDamage(damage, isCritical);
    }

    public override void TakenDamage(float damage, bool isCritical)
    {
        HP = (int)Mathf.Round((float)HP - damage);
        
        var textPos = new Vector3(transform.position.x, transform.position.y, -7);
        damageTextSpawner.ShowDamage(textPos, (int)damage, isCritical);
        
        animController.CrossFade("Hit", 0.1f);

        if (HP <= 0)
        {
            _monsterStateMachine.ChangeState<NMonsterDeathState>();
        }
    }
    
    void Update()
    {
        if (_monsterStateMachine is null)
            Init();
        
        _monsterStateMachine.Run();
    }
    
    private StateMachine _monsterStateMachine;
}
