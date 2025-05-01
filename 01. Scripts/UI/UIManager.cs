using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    void Start()
    {
        _playerData = GameManager.Instance.PlayerData;
        
        playerView.Init(_playerData);
    }
    
    public void OnClickToChangeDamage() => playerView.Presenter.OnChangeDamage();
    public void OnClickToChangeHP() => playerView.Presenter.OnChangeHP();
    public void OnClickToChangeSpeed() => playerView.Presenter.OnChangeSpeed();
    public void OnClickToChangeCriticalProbability() => playerView.Presenter.OnChangeCriticalProbability();
    public void OnClickToChangeCriticalDamage() => playerView.Presenter.OnChangeCriticalDamage();
    
    private PlayerData _playerData;
    [SerializeField] private PlayerView playerView;
}