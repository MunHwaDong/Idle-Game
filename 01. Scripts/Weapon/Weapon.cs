using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    void Awake()
    {
        EventBus.RegisterEvent(GameEventType.GAME_START, Init);
    }
    
    public abstract void Init();
    
    public virtual void OnDamageField()
    {
        damageField.OnDamageCollider();
    }
    
    public virtual void OffDamageField()
    {
        damageField.OffDamageCollider();
    }
    
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        var msg = new EventMessage("HitMonster");

        msg.AddParameter<float>(damageField.GetCalculatedDamage(out var isCritical));
        msg.AddParameter<bool>(isCritical);
        
        EventManager.Instance.PushEventMessage(msg);
        EventManager.Instance.PublishEventQueue(msg.EventName);
        
        OffDamageField();
    }
    
    protected DamageField damageField;
    protected Collider2D collider2d;
}
