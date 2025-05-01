using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int userId;
    
    public int currentStage;
    public int gold;
    public int dia;

    public float damageLevel;
    public float damagePrice;
    
    public float hpLevel;
    public float hpPrice;
    
    public float speedLevel;
    public float speedPrice;
    
    public float criticalProbabilityLevel;
    public float criticalProbabilityPrice;
    
    public float criticalDamageLevel;
    public float criticalDamagePrice;

    private const float MAX_DAMAGE = 1050f;
    private const float MAX_HP = 10500f;
    private const float MAX_SPEED = 10f;
    private const float MAX_CRITICAL_PROBABILITY = 0.35f;
    private const float MAX_CRITICAL_DAMAGE = 3f;
    
    public event Action OnChangeCurrentStage;
    public event Action<int> OnChangeGold;
    public event Action<int> OnChangeDia;
    
    public event Action<float, float> OnChangeDamage;
    public event Action<float, float> OnChangeHP;
    public event Action<float, float> OnChangeSpeed;
    public event Action<float, float> OnChangeCriticalProbability;
    public event Action<float, float> OnChangeCriticalDamage;

    public void UpdateCurrentStage()
    {
        currentStage++;
        
        OnChangeCurrentStage?.Invoke();
    }
    
    public void UpdateGold(int makeGold)
    {
        gold += makeGold;
        OnChangeGold?.Invoke(gold);
    }
    
    public void UpdateDia(int makeDia)
    {
        dia += makeDia;
        OnChangeDia?.Invoke(dia);
    }
    
    public void UpdateDamage(float abilityPoint, float abilityPrice)
    {
        if (damageLevel > MAX_DAMAGE || gold < abilityPrice)
        {
            return;
        }
            
        damageLevel += abilityPoint;
        damagePrice += abilityPrice;
        
        UpdateGold(-(int)abilityPrice);
        OnChangeDamage?.Invoke(damageLevel, damagePrice);
    }
    
    public void UpdateHP(float abilityPoint, float abilityPrice)
    {
        if (hpLevel > MAX_HP || gold < abilityPrice)
        {
            return;
        }
        
        hpLevel += abilityPoint;
        hpPrice += abilityPrice;
        
        UpdateGold(-(int)abilityPrice);
        OnChangeHP?.Invoke(hpLevel, hpPrice);
    }
    
    public void UpdateSpeed(float abilityPoint, float abilityPrice)
    {
        if (speedLevel > MAX_SPEED || gold < abilityPrice)
        {
            return;
        }
        
        speedLevel += abilityPoint;
        speedPrice += abilityPrice;
        
        UpdateGold(-(int)abilityPrice);
        OnChangeSpeed?.Invoke(speedLevel, speedPrice);
    }
    
    public void UpdateCriticalProbability(float abilityPoint, float abilityPrice)
    {
        if (criticalProbabilityLevel > MAX_CRITICAL_PROBABILITY || gold < abilityPrice)
        {
            return;
        }
        
        criticalProbabilityLevel += abilityPoint;
        criticalProbabilityPrice += abilityPrice;
        
        UpdateGold(-(int)abilityPrice);
        OnChangeCriticalProbability?.Invoke(criticalProbabilityLevel, criticalProbabilityPrice);
    }
    
    public void UpdateCriticalDamage(float abilityPoint, float abilityPrice)
    {
        if (criticalDamageLevel > MAX_CRITICAL_DAMAGE || gold < abilityPrice)
        {
            return;
        }
        
        criticalDamageLevel += abilityPoint;
        criticalDamagePrice += abilityPrice;
        
        UpdateGold(-(int)abilityPrice);
        OnChangeCriticalDamage?.Invoke(criticalDamageLevel, criticalDamagePrice);
    }
    
    public PlayerData(int userId, int currentStage, int gold, int dia,
                        float damage, float hp, float speed, float criticalProbability, float criticalDamage)
    {
        this.userId = userId;
        this.currentStage = currentStage;
        this.gold = gold;
        this.dia = dia;
        
        this.damageLevel = damage;
        this.hpLevel = hp;
        this.speedLevel = speed;
        this.criticalProbabilityLevel = criticalProbability;
        this.criticalDamageLevel = criticalDamage;
    }
}