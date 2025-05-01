using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    new void Awake()
    {
        base.Awake();
        
        _dataLocalPath = Path.Combine(Application.persistentDataPath, "");
    }
    
    public PlayerData LoadPlayerData()
    {
        //string[] foundPlayerDataPath = Directory.GetFiles(_dataLocalPath, _pattern);
        string path = Path.Combine(_dataLocalPath, "PlayerData.json");

        if (File.Exists(path))
        {
            string userData = File.ReadAllText(path);
            
            var playerData = JsonUtility.FromJson<PlayerData>(userData);
            
            PlayerDamageData.damage = playerData.damageLevel;
            PlayerDamageData.criticalProbability = playerData.criticalProbabilityLevel;
            PlayerDamageData.criticalDamage = playerData.criticalDamageLevel;
            
            return playerData;
        }

        throw new FileNotFoundException("유저 정보를 찾을 수 없습니다.");
    }
    
    private readonly string _pattern = "PlayerData.json";
    private string _dataLocalPath;
    
    public DamageData PlayerDamageData { get; } = new DamageData();
}
