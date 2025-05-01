using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewbieSwordPhysicsDamage : DamageHandler
{
    public override float HandleDamage(float damage, DamageData damageData)
    {
        float rand = Random.Range(LowerDamageThreshold / 100f, UpperDamageThreshold / 100f);

        damage += (rand * 100f);

        return nextHandler?.HandleDamage(damage, damageData) ?? damage;
    }
    
    //뉴비 소드의 추가 하한 데미지
    private const float LowerDamageThreshold = 20f;
    //뉴비 소드의 추가 상한 데미지
    private const float UpperDamageThreshold = 30f;
}
