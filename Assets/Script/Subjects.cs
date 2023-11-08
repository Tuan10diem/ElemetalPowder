using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Subjects : MonoBehaviour
{
    private List<IObserver> _observers = new List<IObserver>();

    public void AddObservoer(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        _observers.Remove(observer);
    }

    protected void NotifyObservers(PlayerAction action)
    {
        _observers.ForEach((_observers) =>
        {
            _observers.OnNotify(action);
        });
    }
    
    protected void NotifyObservers(BossAction action)
    {
        _observers.ForEach((_observers) =>
        {
            _observers.OnNotify(action);
        });
    }
}
