using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : Subjects
{

    public float affectTimeOfItem = 5f;
    public float affectTimeOfWeapon = 10f;
    
    public float speedInit = 5f;
    public float speedRealTime;
    public int HP = 5;
    public int maxHP = 5;
    public bool shield = false;
    public int explosionRadiusReal = 3;
    public int bombAmount = 3;

    public GameObject spinningAxe;
    public GameObject axeIcon;
    public GameObject excalibur;
    public GameObject excaliburIcon;
    public GameObject speedIncreaseIcon;
    public GameObject shieldIcon;
    
    public void SpeedIncrease(float moreSpeed)
    {
        speedRealTime = speedInit+moreSpeed;
        StartCoroutine(this.GetComponent<PlayerMovement>().SpeedChange(speedInit, speedRealTime, affectTimeOfItem));
        StartCoroutine(SpeedIcon());
        NotifyObservers(PlayerAction.SpeedIncrease,0);
    }

    private IEnumerator SpeedIcon()
    {
        speedIncreaseIcon.SetActive(true);
        yield return new WaitForSeconds(affectTimeOfItem);
        speedIncreaseIcon.SetActive(false);
    }

    public void HandleHurt(int damage)
    {
        if (shield)
        {
            Debug.Log("Tao co khien roi");
        }
        else
        {
            HP -= damage;
            NotifyObservers(PlayerAction.Hurt,damage);
        }
    }

    public void HandleHeal(int HP)
    {
        this.HP = Mathf.Min(this.HP+HP, maxHP);
        NotifyObservers(PlayerAction.Heal,HP);
    }

    public void HandleShield()
    {
        NotifyObservers(PlayerAction.Shield,0);
        StartCoroutine(Shield(affectTimeOfItem));
    }

    public IEnumerator Shield(float affectTime)
    {
        shield = true;
        shieldIcon.SetActive(true);
        yield return new WaitForSeconds(affectTime);
        shield=false;
        shieldIcon.SetActive(false);
    }

    public void HandleBlastRadius(int More)
    {
        int expRadiusAfterBuff = explosionRadiusReal + More;
        StartCoroutine(GetComponent<BombController>().BlastRadius(explosionRadiusReal,expRadiusAfterBuff,affectTimeOfItem));
        NotifyObservers(PlayerAction.BlastRadius,More);
    }

    public void PlaceBomb()
    {
        NotifyObservers(PlayerAction.PlaceBomb,1);
    }

    public void PlusBomb()
    {
        NotifyObservers(PlayerAction.PlusBomb,1);
    }

    public void HandleSpinningAxe()
    {
        StartCoroutine(SpinningAxe());
        //NotifyObservers()
    }

    public IEnumerator SpinningAxe()
    {
        spinningAxe.SetActive(true);
        axeIcon.SetActive(true);
        yield return new WaitForSeconds(affectTimeOfWeapon);
        spinningAxe.SetActive(false);
        axeIcon.SetActive(false) ;

    }

    public void HandleExcalibur()
    {
        StartCoroutine (Excalibur());
        //NotifyObservers()
    }

    public IEnumerator Excalibur()
    {
        excalibur.SetActive(true);
        excaliburIcon.SetActive(true);
        yield return new WaitForSeconds(affectTimeOfWeapon);
        excalibur.SetActive(false);
        excalibur.SetActive(false);
    }

}
