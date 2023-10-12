using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : Subjects
{

    public float affectTimeOfItem = 5f;
    
    public float speedInit = 5f;
    public float speedRealTime;
    public int HP = 5;
    public int maxHP = 5;
    public bool shield = false;
    public int explosionRadiusReal = 3;
    
    public void SpeedIncrease(float moreSpeed)
    {
        speedRealTime = speedInit+moreSpeed;
        StartCoroutine(this.GetComponent<PlayerMovement>().SpeedChange(speedInit, speedRealTime, affectTimeOfItem));
        NotifyObservers(PlayerAction.SpeedIncrease);
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
            NotifyObservers(PlayerAction.Hurt);
        }
    }

    public void HandleHeal(int HP)
    {
        this.HP = Mathf.Min(this.HP+HP, maxHP);
        NotifyObservers(PlayerAction.Heal);
    }

    public void HandleShield()
    {
        NotifyObservers(PlayerAction.Shield);
        StartCoroutine(Shield(affectTimeOfItem));
    }

    public IEnumerator Shield(float affectTime)
    {
        shield = true;
        yield return new WaitForSeconds(affectTime);
        shield=false;
    }

    public void HandleBlastRadius(int More)
    {
        int expRadiusAfterBuff = explosionRadiusReal + More;
        StartCoroutine(GetComponent<BombController>().BlastRadius(explosionRadiusReal,expRadiusAfterBuff,affectTimeOfItem));
        NotifyObservers(PlayerAction.BlastRadius);
    }

    public void PlaceBomb()
    {
        NotifyObservers(PlayerAction.PlaceBomb);
    }
}
