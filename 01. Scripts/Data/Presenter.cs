using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Presenter
{
    public Presenter(IPlayerView view, PlayerData model)
    {
        _view = view;
        _model = model;
        
        _model.OnChangeGold += _view.SetGold;
        _model.OnChangeDia += _view.SetDia;

        _model.OnChangeDamage += _view.SetDamage;
        _model.OnChangeHP += _view.SetHP;
        _model.OnChangeSpeed += _view.SetSpeed;
        _model.OnChangeCriticalProbability += _view.SetCriticalProbability;
        _model.OnChangeCriticalDamage += _view.SetCriticalDamage;
        
        Init();
    }
    
    public void OnChangeDamage()
    {
        _model.UpdateDamage(10, _model.damagePrice);
    }
    
    public void OnChangeHP()
    {
        _model.UpdateHP(100, _model.hpPrice);
    }
    
    public void OnChangeSpeed()
    {
        _model.UpdateSpeed(0.2f, _model.speedPrice);
    }
    
    public void OnChangeCriticalProbability()
    {
        _model.UpdateCriticalProbability(0.01f, _model.criticalProbabilityPrice);
    }
    
    public void OnChangeCriticalDamage()
    {
        _model.UpdateCriticalDamage(0.05f, _model.criticalDamagePrice);
    }

    private void Init()
    {
        _view.SetGold(_model.gold);
        _view.SetDia(_model.dia);
        
        _view.SetDamage(_model.damageLevel, _model.damagePrice);
        _view.SetHP(_model.hpLevel, _model.hpPrice);
        _view.SetSpeed(_model.speedLevel, _model.speedPrice);
        _view.SetCriticalProbability(_model.criticalProbabilityLevel, _model.criticalProbabilityPrice);
        _view.SetCriticalDamage(_model.criticalDamageLevel, _model.criticalDamagePrice);
    }
    
    private readonly IPlayerView _view;
    private readonly PlayerData _model;
}
