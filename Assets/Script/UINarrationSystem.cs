using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINarrationSystem : MonoBehaviour, IObserver
{

    public Image healthBar;
    public int healthAmount=10;
    public int currentHealth;

    public int bombAmount;
    public int currentBomb;
    public Text bombNumber;

    public int radiusInit;
    public int currentRadius;
    public Text radiusText;

    public Subjects _playerSubject;
    public PlayerStatus _playerStatus;
    
    private Dictionary<PlayerAction, System.Action<float>> _playerActionHandler;
    
    private void Awake()
    {
        bombNumber.text = currentBomb.ToString();
        radiusText.text = currentRadius.ToString();
        radiusInit = _playerStatus.GetComponent<PlayerStatus>().explosionRadiusReal;
        currentRadius = radiusInit;
        bombAmount = _playerStatus.GetComponent<PlayerStatus>().bombAmount;
        currentBomb = bombAmount;

        _playerActionHandler = new Dictionary<PlayerAction, Action<float>>()
        {
            { PlayerAction.Hurt,(n) => HandleHurt(n) },
            // { PlayerAction.SpeedIncrease, HandleSpeedIncrease },
            // { PlayerAction.Shield, HandleShield },
            { PlayerAction.BlastRadius,(n)=> HandleBlastRadius(n) },
            // { PlayerAction.HandleBomb, HandleHandleBomb },
            { PlayerAction.Heal, (n)=> HandleHeal(n)},
            { PlayerAction.PlaceBomb, (n) =>PlaceBomb(n)},
            { PlayerAction.PlusBomb, (n) =>PlusBomb(n)}
        };
    }
    
    public void OnNotify(PlayerAction action, float n)
    {
        if (_playerActionHandler.ContainsKey(action))
        {
            _playerActionHandler[action](n);
        }
    }
    
    public void OnNotify(BossAction action, float n)
    {
    }

    private void HandleHurt(float n)
    {
        currentHealth -= (int) n;
        if (healthAmount!=0) healthBar.fillAmount = currentHealth/ ((float)1.0 * healthAmount);

        Debug.Log("- mau o thanh HP ne");
    }

    private void HandleHeal(float n)
    {
        currentHealth = Mathf.Min(currentHealth + (int) n, healthAmount);
        if (healthAmount != 0) healthBar.fillAmount = currentHealth / ((float)1.0 * healthAmount);
        Debug.Log("+ mau o thanh HP ne");
    }

    private void PlaceBomb(float n)
    {
        currentBomb -= 1;
        bombNumber.text = currentBomb.ToString();
        Debug.Log("-1 o thanh bomb Amount ne");
    }

    private void PlusBomb(float n)
    {
        currentBomb = Mathf.Min(currentBomb + 1, bombAmount);
        bombNumber.text = currentBomb.ToString();
        Debug.Log("+1 o thanh Bomb Amount ne");
    }

    private void HandleBlastRadius(float n)
    {
        StartCoroutine(BlastRadius(n));
        Debug.Log("+ o radius ne");
    }
    private IEnumerator BlastRadius(float n)
    {
        currentRadius += (int) n;
        radiusText.text = currentRadius.ToString();
        yield return new WaitForSeconds(_playerStatus.GetComponent<PlayerStatus>().affectTimeOfItem);
        currentRadius = radiusInit;
        radiusText.text = currentRadius.ToString();
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
