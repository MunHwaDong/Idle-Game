using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class MonsterPool : MonoBehaviour
{
    void Awake()
    {
        EventBus.RegisterEvent(GameEventType.IDLE, SpawnMonster);
    }
    
    private IObjectPool<IMonster> pool
    {
        get
        {
            if(_pool is null)
            {
                _pool = new ObjectPool<IMonster>(
                    CreateMonster,
                    TakeFromPool,
                    ReturnedPool,
                    DestroyPoolObj,
                    true,
                    stackCapacity,
                    maxPoolSize);
            }
            return _pool;
        }
    }
    
    private IObjectPool<IMonster> _pool;

    private IMonster CreateMonster()
    {
        int randIdx = Random.Range(0, monsterPrefab.Count);
        var obj = Instantiate(monsterPrefab[randIdx]);

        IMonster monster = obj.GetComponent<IMonster>();

        monster.name = monsterPrefab[randIdx].name;
        monster.pool = pool;

        return monster;
    }

    private void TakeFromPool(IMonster monster)
    {
        monster.gameObject.SetActive(true);
    }

    private void ReturnedPool(IMonster monster)
    {
        monster.gameObject.SetActive(false);
    }

    private void DestroyPoolObj(IMonster monster)
    {
        Destroy(monster.gameObject);
    }

    private void SpawnMonster()
    {
        var monster = pool.Get();

        monster.transform.position = transform.position;

        GameManager.Instance.spawnMonsterAmount--;
        
        GameManager.Instance.UpdateProgressBar();
        
        if (GameManager.Instance.spawnMonsterAmount < 0)
        {
            GameManager.Instance.PlayerData.UpdateCurrentStage();
        }
    }
    
    [SerializeField] private List<GameObject> monsterPrefab;

    private readonly int stackCapacity = 15;
    private readonly int maxPoolSize = 15;
}