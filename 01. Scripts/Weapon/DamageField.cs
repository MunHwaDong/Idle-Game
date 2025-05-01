using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class DamageField
{
    public float GetCalculatedDamage(out bool isCritical)
    {
        if (damageHandler is not null)
        {
            var calcDamage = damageHandler.HandleDamage(damageData.damage, damageData);
            isCritical = damageData.isCritical;
            return calcDamage;
        }

        isCritical = damageData.isCritical;
        return damageData.damage;
    }
    
    public void OnDamageCollider()
    {
        damageFieldCollider.isTrigger = true;
    }
    
    public void OffDamageCollider()
    {
        damageFieldCollider.isTrigger = false;
    }
    
    public readonly DamageData damageData = new();
    
    public Collider2D damageFieldCollider;
    public DamageHandler damageHandler;
}