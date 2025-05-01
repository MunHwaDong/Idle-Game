using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerView : MonoBehaviour, IPlayerView
{
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI diaText;

    [SerializeField] private TextMeshProUGUI damageLevelText;
    [SerializeField] private TextMeshProUGUI damagePriceText;
    
    [SerializeField] private TextMeshProUGUI hpLevelText;
    [SerializeField] private TextMeshProUGUI hpPriceText;
    
    [SerializeField] private TextMeshProUGUI speedLevelText;
    [SerializeField] private TextMeshProUGUI speedPriceText;
    
    [SerializeField] private TextMeshProUGUI critProbLevelText;
    [SerializeField] private TextMeshProUGUI critProbPriceText;
    
    [SerializeField] private TextMeshProUGUI critDamageLevelText;
    [SerializeField] private TextMeshProUGUI critDamagePriceText;
    
    public void Init(PlayerData data)
    {
        Presenter = new Presenter(this, data);
    }
    
    public Presenter Presenter { get; private set; }
    public void SetGold(int value) => goldText.text = value.ToString();
    public void SetDia(int value) => diaText.text = value.ToString();
    public void SetDamage(float levelValue, float priceValue)
    {
        int index = damageLevelText.text.IndexOf(' ');
        damageLevelText.text = damageLevelText.text.Substring(0, index + 1) + levelValue.ToAbbreviatedString();
        
        index = damagePriceText.text.IndexOf(' ');
        damagePriceText.text = damagePriceText.text.Substring(0, index + 1) + priceValue.ToAbbreviatedString();
    }

    public void SetHP(float levelValue, float priceValue)
    {
        int index = hpLevelText.text.IndexOf(' ');
        hpLevelText.text = hpLevelText.text.Substring(0, index + 1) + levelValue.ToAbbreviatedString();
        
        index = hpPriceText.text.IndexOf(' ');
        hpPriceText.text = hpPriceText.text.Substring(0, index + 1) + priceValue.ToAbbreviatedString();
    }

    public void SetSpeed(float levelValue, float priceValue)
    {
        int index = speedLevelText.text.IndexOf(' ');
        speedLevelText.text = speedLevelText.text.Substring(0, index + 1) + levelValue.ToAbbreviatedString();
        
        index = speedPriceText.text.IndexOf(' ');
        speedPriceText.text = speedPriceText.text.Substring(0, index + 1) + priceValue.ToAbbreviatedString();
    }

    public void SetCriticalProbability(float levelValue, float priceValue)
    {
        int index = critProbLevelText.text.IndexOf(' ');
        critProbLevelText.text = critProbLevelText.text.Substring(0, index + 1) + $"{levelValue:P1}";
        
        index = critProbPriceText.text.IndexOf(' ');
        critProbPriceText.text = critProbPriceText.text.Substring(0, index + 1) + priceValue.ToAbbreviatedString();
    }

    public void SetCriticalDamage(float levelValue, float priceValue)
    {
        int index = critDamageLevelText.text.IndexOf(' ');
        critDamageLevelText.text = critDamageLevelText.text.Substring(0, index + 1) + levelValue.ToAbbreviatedString();
        
        index = critDamagePriceText.text.IndexOf(' ');
        critDamagePriceText.text = critDamagePriceText.text.Substring(0, index + 1) + priceValue.ToAbbreviatedString();
    }
}
