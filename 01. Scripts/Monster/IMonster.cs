using System;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

public abstract class IMonster : MonoBehaviour, IOnEvent
{
    void OnEnable()
    {
        monsterNo = 1001;
        
        Init();
    }
    
    protected virtual void Init()
    {
        Collider2D collider2d;
        
        //Instantiate될 때 한 번만 초기화
        if(Blackboard is null)
        {
            yScreenHalfSize = Camera.main.orthographicSize;
            xScreenHalfSize = yScreenHalfSize * Camera.main.aspect;

            damageTextSpawner = FindObjectOfType<DamageTextSpawner>();

            HP = Convert.ToInt32(GameTableManager.Instance.MonsterTable.GetMonsterData(monsterNo).monsterHP);
            stageHP = Convert.ToInt32(GameTableManager.Instance.StageDataTable.GetStageData(GameManager.Instance.PlayerData.currentStage).stageHP);
            HP += ((GameManager.Instance.PlayerData.currentStage - ID_CORRECTION) * stageHP);
            
            Blackboard = new Blackboard();
            
            collider2d = transform.GetComponent<Collider2D>();
            
            Blackboard.WriteData(typeof(Collider2D), collider2d);
            Blackboard.WriteData(typeof(IMonster), this);

            collider2d.includeLayers = 
                (1 << LayerMask.NameToLayer("Player")) | (1 << LayerMask.NameToLayer("Weapon"));
            
            EventManager.Instance.AddListener("HitMonster", this, gameObject);
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Monster");
            collider2d = Blackboard.ReadData<Collider2D>();
            collider2d.enabled = true;
        }
        
        EventManager.Instance.AddListener("HitMonster", this, gameObject);
    }
    
    public abstract void OnEvent(EventMessage msg);
    
    public abstract void TakenDamage(float damage, bool isCritical);
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        EventBus.Publish(GameEventType.ENCOUNTER);
    }
    
    public virtual void ResetMonster()
    {
        HP = Convert.ToInt32(GameTableManager.Instance.MonsterTable.GetMonsterData(monsterNo).monsterHP);
        stageHP = Convert.ToInt32(GameTableManager.Instance.StageDataTable.GetStageData(GameManager.Instance.PlayerData.currentStage).stageHP);
        HP += ((GameManager.Instance.PlayerData.currentStage - ID_CORRECTION) * stageHP);
    }

    public virtual void ReturnToPool()
    {
        try
        {
            pool.Release(this);
        }
        catch (Exception e)
        {
            Debug.LogWarning("이미 Release된 Object 입니다.");
        }
    }
    
    public void OnDeathAnimationEnd()
    {
        isDying = false;
    }

    public int HP;
    public int stageHP;
    public int monsterNo;
    public const int ID_CORRECTION = 1000;
    
    public float yScreenHalfSize = 0;
    public float xScreenHalfSize = 0;
    
    public bool isDying = false;
    
    public IObjectPool<IMonster> pool { get; set; }
    
    protected Animator animController;
    public Blackboard Blackboard { get; private set; } = null;

    protected DamageTextSpawner damageTextSpawner;
}