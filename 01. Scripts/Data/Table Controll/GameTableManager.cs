using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTableManager : Singleton<GameTableManager>
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        ParsingTable();
    }

    void ParsingTable()
    {
        stageDataTable.Parsing(gameTablePath + "/Stage");
        monsterTable.Parsing(gameTablePath + "/Monster");
    }
    
    private readonly string gameTablePath = "GameTable";

    private StageTable stageDataTable = new StageTable();
    private MonsterTable monsterTable = new MonsterTable();

    public StageTable StageDataTable => stageDataTable;
    public MonsterTable MonsterTable => monsterTable;
}