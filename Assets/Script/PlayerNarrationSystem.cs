using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNarrationSystem : MonoBehaviour, IObserver
{
    public Subjects _playerSubject;

    private Dictionary<PlayerAction, System.Action> _playerActionHandler;

    private void Awake()
    {
        _playerActionHandler = new Dictionary<PlayerAction, Action>()
        {
            { PlayerAction.Hurt, HandleHurt },
            { PlayerAction.SpeedIncrease, HandleSpeedIncrease },
            { PlayerAction.Shield, HandleShield },
            { PlayerAction.BlastRadius, HandleBlastRadius },
            { PlayerAction.HandleBomb, HandleHandleBomb }
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
        Debug.Log("I'm so fucking tired bro");
    }

    private void HandleShield()
    {
        Debug.Log("No fucking fighting");
    }

    private void HandleHandleBomb()
    {
        Debug.Log("Dont fook with Peaky Blinder");
    }

    private void HandleSpeedIncrease()
    {
        Debug.Log("Speed Up man");
    }

    private void HandleBlastRadius()
    {
        Debug.Log("Powpow powpow");
    }

    private void OnEnable()
    {
        _playerSubject.AddObservoer(this);
    }

    protected void OnDisable()
    {
        _playerSubject.RemoveObserver(this);
    }
}
