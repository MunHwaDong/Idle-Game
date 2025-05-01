using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterData
{
    public string monsterNo;
    public string monsterHP;
    public string monsterAttk;
}

[System.Serializable]
public class MonsterDataRow
{
    public string monsterNo;
    public string monsterHP;
    public string monsterAttk;
}

public class MonsterTable : BaseTable
{
    public override void Parsing(string jsonPath)
    {
        base.Parsing(jsonPath);

        monsterTableRows = JsonHelper.FromJson<MonsterDataRow>(json);
        monsterDataDict = ConvertListToDict();
    }
    
    private Dictionary<int, MonsterData> ConvertListToDict()
    {
        Dictionary<int, MonsterData> dict = new Dictionary<int, MonsterData>();

        foreach(var row in monsterTableRows)
        {
            int key = System.Convert.ToInt32(row.monsterNo);

            MonsterData monsterData = new MonsterData();
            monsterData.monsterNo = row.monsterNo;
            monsterData.monsterHP = row.monsterHP;
            monsterData.monsterAttk = row.monsterAttk;

            if (!dict.ContainsKey(key))
                dict.Add(key, monsterData);
        }

        return dict;
    }

    public MonsterData GetMonsterData(int monsterNo)
    {
        if(monsterDataDict.ContainsKey(monsterNo))
            return monsterDataDict[monsterNo];

        return null;
    }
    
    MonsterDataRow[] monsterTableRows;
    Dictionary<int, MonsterData> monsterDataDict = new Dictionary<int, MonsterData>();
}
