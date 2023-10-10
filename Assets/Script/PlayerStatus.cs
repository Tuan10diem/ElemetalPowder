using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : Subjects
{

    public float speedInit = 5f;
    public float speedRealTime;
    public int HP = 5;
    public int maxHP = 5;
    public bool shield = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SpeedIncrease(float moreSpeed, float affectTime)
    {
        speedRealTime = speedInit+moreSpeed;
        StartCoroutine(this.GetComponent<PlayerMovement>().SpeedChange(speedInit, speedRealTime, affectTime));
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

    public void HandleShield(float affectTime)
    {
        NotifyObservers(PlayerAction.Shield);
        StartCoroutine(Shield(affectTime));
    }

    public IEnumerator Shield(float affectTime)
    {
        shield = true;
        
        yield return new WaitForSeconds(affectTime);
        shield=false;
    }
}
