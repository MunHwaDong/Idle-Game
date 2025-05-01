using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewbieSwordCriticalDamage : DamageHandler
{
    public override float HandleDamage(float damage, DamageData damageData)
    {
        float rand = Random.value;

        if (rand <= damageData.criticalProbability + CriProbCorrection)
        {
            damageData.isCritical = true;
            damage += (damage * damageData.criticalDamage);
        }
        else
        {
            damageData.isCritical = false;
            damage += 0;
        }

        return nextHandler?.HandleDamage(damage, damageData) ?? damage;
    }

    //뉴비 소드의 크리티컬 확률 증가치
    private const float CriProbCorrection = 0.05f;
}
