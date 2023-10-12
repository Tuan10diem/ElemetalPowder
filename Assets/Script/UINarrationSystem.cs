using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINarrationSystem : MonoBehaviour, IObserver
{

    public Subjects _playerSubject;
    
    private Dictionary<PlayerAction, System.Action> _playerActionHandler;
    
    private void Awake()
    {
        _playerActionHandler = new Dictionary<PlayerAction, Action>()
        {
            { PlayerAction.Hurt, HandleHurt },
            // { PlayerAction.SpeedIncrease, HandleSpeedIncrease },
            // { PlayerAction.Shield, HandleShield },
            // { PlayerAction.BlastRadius, HandleBlastRadius },
            // { PlayerAction.HandleBomb, HandleHandleBomb },
            { PlayerAction.Heal, HandleHeal},
            { PlayerAction.PlaceBomb, PlaceBomb}
        };
    }
    
    public void OnNotify(PlayerAction action)
    {
        if (_playerActionHandler.ContainsKey(action))
        {
            _playerActionHandler[action]();
        }
    }

    private void HandleHurt()
    {
        Debug.Log("- mau o thanh HP ne");
    }

    private void HandleHeal()
    {
        Debug.Log("+ mau o thanh HP ne");
    }

    private void PlaceBomb()
    {
        Debug.Log("-1 o thanh bomb Amount ne");
    }

    private void OnEnable()
    {
        _playerSubject.AddObservoer(this);
    }

    private void OnDisable()
    {
        _playerSubject.RemoveObserver(this);
    }
}
