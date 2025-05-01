public abstract class DamageHandler
{
    protected DamageHandler nextHandler;

    public DamageHandler SetNextHandler(DamageHandler handler)
    {
        this.nextHandler = handler;
        return this.nextHandler;
    }
    
    public abstract float HandleDamage(float damage, DamageData damageData);
}