public interface IPlayerView
{
    void SetGold(int value);
    void SetDia(int value);
    
    void SetDamage(float levelValue, float priceValue);
    void SetHP(float levelValue, float priceValue);
    void SetSpeed(float levelValue, float priceValue);
    void SetCriticalProbability(float levelValue, float priceValue);
    void SetCriticalDamage(float levelValue, float priceValue);
}