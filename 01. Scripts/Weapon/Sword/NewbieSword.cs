using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewbieSword : Weapon
{
    public override void Init()
    {
        DamageFieldBuilder builder = new DamageFieldBuilder();
        
        playerDamage = DataManager.Instance.PlayerDamageData;

        GameManager.Instance.PlayerData.OnChangeDamage += UpdateDamage;
        GameManager.Instance.PlayerData.OnChangeCriticalDamage += UpdatecriticalDamage;
        GameManager.Instance.PlayerData.OnChangeCriticalProbability += UpdatecriticalProbability;
        
        List<DamageHandler> damageHandlers = new List<DamageHandler>();

        if (TryGetComponent(out collider2d) is false)
            throw new Exception("무기 오브젝트에 Collider2D Component가 없습니다.");
        else
        {
            collider2d.includeLayers = LayerMask.NameToLayer("Monster");
            collider2d.excludeLayers = LayerMask.NameToLayer("Death");
            collider2d.gameObject.layer = LayerMask.NameToLayer("Weapon");
        }
        
        damageHandlers.Add(new NewbieSwordPhysicsDamage());
        damageHandlers.Add(new NewbieSwordCriticalDamage());

        builder.SetCollider(collider2d).
            SetDamage(playerDamage.damage).
            SetCriticalDamage(playerDamage.criticalDamage).
            SetCriticalProbability(playerDamage.criticalProbability).
            SetDamageHandler(damageHandlers.ToArray());
        
        damageField = builder.Build();
    }
    
    public void UpdateDamage(float point = 0, float dummy = 0)
    {
        playerDamage.damage += point;
        
        DamageFieldBuilder builder = new DamageFieldBuilder();

        builder.SetCollider(collider2d).
            SetDamage(playerDamage.damage).
            SetCriticalDamage(playerDamage.criticalDamage).
            SetCriticalProbability(playerDamage.criticalProbability).
            SetDamageHandler(damageHandlers.ToArray());
        
        damageField = builder.Build();
    }
    public void UpdatecriticalDamage(float point = 0, float dummy = 0)
    {
        playerDamage.criticalDamage = point;
        
        DamageFieldBuilder builder = new DamageFieldBuilder();

        builder.SetCollider(collider2d).
            SetDamage(playerDamage.damage).
            SetCriticalDamage(playerDamage.criticalDamage).
            SetCriticalProbability(playerDamage.criticalProbability).
            SetDamageHandler(damageHandlers.ToArray());
        
        damageField = builder.Build();
    }
    public void UpdatecriticalProbability(float point = 0, float dummy = 0)
    {
        playerDamage.criticalProbability = point;
        
        DamageFieldBuilder builder = new DamageFieldBuilder();

        builder.SetCollider(collider2d).
            SetDamage(playerDamage.damage).
            SetCriticalDamage(playerDamage.criticalDamage).
            SetCriticalProbability(playerDamage.criticalProbability).
            SetDamageHandler(damageHandlers.ToArray());
        
        damageField = builder.Build();
    }

    private List<DamageHandler> damageHandlers = new()
        { new NewbieSwordPhysicsDamage(), new NewbieSwordCriticalDamage() };
    
    private DamageData playerDamage;
}