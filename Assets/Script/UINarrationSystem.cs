using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UINarrationSystem : MonoBehaviour, IObserver
{
    [Header ("Player")]
    public Image healthBar;
    public int healthAmount=10;
    public int currentHealth;
    public bool isWin = false;

    [Header("Boss")]
    public Image bossHealthBar;
    public int bossHealthAmount = 10;
    public int bossCurrentHealth;

    [Header("Bomb")]
    public int bombAmount;
    public int currentBomb;
    public Text bombNumber;

    public int radiusInit;
    public int currentRadius;
    public Text radiusText;

    [Header("UI endgame")]
    public GameObject endGameInfo;
    public Text statement;
    public Image endBg;

    [Header("References")]
    public Subjects _playerSubject;
    public PlayerStatus _playerStatus;
    public Subjects _levelManager;
    public Subjects _bossController;

    private Dictionary<PlayerAction, System.Action<float>> _playerActionHandler;
    private Dictionary<BossAction, System.Action<float>> _bossActionHandler;

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
            { PlayerAction.PlusBomb, (n) =>PlusBomb(n)},
            { PlayerAction.Win, (n) => Win(n) },
            { PlayerAction.Lose, (n) => Lose(n) },
        };

        _bossActionHandler = new Dictionary<BossAction, Action<float>>()
        {
            { BossAction.Hurt,(n) => BossHandleHurt(n) },
            //{ BossAction.Angry,(n)=>  }
        };
    }

    public void ReloadSceneBtn()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void HomeBtn()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("HomeScene");
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    private void Win(float n)
    {
        Debug.Log("before");
        if (!endGameInfo) return;
        if (!isWin) return;
        Debug.Log("Win");
        statement.text = "You Win";
        statement.color = Color.white;
        endGameInfo.SetActive(true);
        Time.timeScale = 0f;
    }

    private void Lose(float n)
    {
        if (!endGameInfo) return;
        Debug.Log("Lose");
        statement.text = "You Lose";
        statement.color = new Color(255f / 255f, 0f, 61f / 255f);
        endGameInfo.SetActive(true);
        Time.timeScale = 0f;

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
        if (_bossActionHandler.ContainsKey(action))
        {
            _bossActionHandler[action](n);
        }
    }

    private void HandleHurt(float n)
    {
        currentHealth -= (int) n;
        if (healthAmount!=0) healthBar.fillAmount = currentHealth/ ((float)1.0 * healthAmount);

        Debug.Log("- mau o thanh HP ne");
    }

    private void BossHandleHurt(float n)
    {
        bossCurrentHealth -= (int)n;
        if (bossHealthAmount != 0) bossHealthBar.fillAmount = bossCurrentHealth / ((float)1.0 * bossHealthAmount);

        Debug.Log("- mau boss");
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
        _levelManager.AddObservoer(this);
        _bossController.AddObservoer(this);
    }

    private void OnDisable()
    {
        _playerSubject.RemoveObserver(this);
        _levelManager.RemoveObserver(this);
        _bossController.RemoveObserver(this);   
    }
}
