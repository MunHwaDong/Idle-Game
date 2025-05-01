using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFieldBuilder
{
    public DamageFieldBuilder()
    {
        _damageField = new DamageField();
    }

    public DamageFieldBuilder SetCollider(Collider2D collider)
    {
        _damageField.damageFieldCollider = collider;
        return this;
    }

    public DamageFieldBuilder SetDamage(float damage)
    {
        _damageField.damageData.damage = damage;
        return this;
    }

    public DamageFieldBuilder SetCriticalDamage(float criticalDamage)
    {
        _damageField.damageData.criticalDamage = criticalDamage;
        return this;
    }

    public DamageFieldBuilder SetCriticalProbability(float criticalDamageProbability)
    {
        _damageField.damageData.criticalProbability = criticalDamageProbability;
        return this;
    }

    public DamageFieldBuilder SetDamageHandler(DamageHandler[] damageHandler)
    {
        _damageField.damageHandler = damageHandler[0];
        
        DamageHandler handler = _damageField.damageHandler;

        for (int i = 1; i < damageHandler.Length; i++)
        {
            handler = handler.SetNextHandler(damageHandler[i]);
        }

        return this;
    }

    public DamageField Build()
    {
        return _damageField;
    }
    
    private readonly DamageField _damageField;
}