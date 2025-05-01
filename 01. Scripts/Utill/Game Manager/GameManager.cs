using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : Singleton<GameManager>
{
    new void Awake()
    {
        base.Awake();
        
        _gameStateMachine ??= Factory.CreateItems<GameStateFactory>() as StateMachine;
        
        _monsterPool = FindObjectOfType<MonsterPool>();
    }

    void Start()
    {
        if(_gameStateMachine is null)
            _gameStateMachine = Factory.CreateItems<GameStateFactory>() as StateMachine;
        
        _gameStateMachine.ChangeState<StartState>();
    }

    void Update()
    {
        if(_gameStateMachine is null)
            _gameStateMachine = Factory.CreateItems<GameStateFactory>() as StateMachine;
        
        _gameStateMachine.Run();
    }
    
    public void InitPlayerData()
    {
        PlayerData = DataManager.Instance.LoadPlayerData();

        PlayerData.OnChangeCurrentStage += NextStage;
        
        NextStage();
    }

    public void NextStage()
    {
        spawnMonsterAmount = Random.Range(15, 31);
        
        stageProgressBar.Init(spawnMonsterAmount);
    }

    public void UpdateProgressBar()
    {
        stageProgressBar.OnMonsterKilled();
    }
    
    void OnDestroy()
    {
        //TODO: 플레이 데이터 기록하기
    }

    public bool monsterDeathFlag = false;
    public int spawnMonsterAmount = 0;

    public PlayerData PlayerData { get; private set; }

    private MonsterPool _monsterPool;
    
    private StateMachine _gameStateMachine;
    
    [SerializeField] private StageProgressBar stageProgressBar;
}
