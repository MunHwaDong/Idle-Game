using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;

public class NMonsterDeathState : IState
{
    public NMonsterDeathState(StateMachine stateMachine)
    {
        FSM = stateMachine;
    }
    
    public async void EnterState()
    {
        _animator ??= FSM.Blackboard.ReadData<Animator>();
        _transform ??= FSM.Blackboard.ReadData<Transform>();
        _monster ??= FSM.Blackboard.ReadData<IMonster>();
        
        _transform.gameObject.layer = LayerMask.NameToLayer("Death");
        
        _animator.Play("Death");
        _monster.isDying = true;

        int makeGold = Convert.ToInt32(GameTableManager.Instance.StageDataTable.GetStageData(_monster.monsterNo).rewardGold);
        GameManager.Instance.monsterDeathFlag = true;
        GameManager.Instance.PlayerData.UpdateGold(makeGold);
        
        FSM.Blackboard.ReadData<Collider2D>().enabled = false;
        EventManager.Instance.RemoveListener("HitMonster", _monster, _monster.gameObject);
        
        await WaitDeathAnimation();
    }

    public void UpdateState()
    {
        _transform.position += Vector3.left * (Time.deltaTime * 4f);
    }

    public void ExitState()
    {
        
    }

    private async UniTask WaitDeathAnimation()
    {
        while (_monster.isDying is true || _transform.position.x > -_monster.xScreenHalfSize)
        {
            await UniTask.Yield();
        }
        
        _monster.ResetMonster();
        _monster.ReturnToPool();
    }

    public StateMachine FSM { get; }
    
    private Animator _animator;
    private Transform _transform;
    
    private IMonster _monster;
}
