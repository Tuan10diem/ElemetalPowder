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
    int radiusRealtime = 3;
    public int bombAmount = 3;
    public float moreSpeed;
    public int moreRadius;
    

    public Dictionary<Item, float> numberOfItem = new Dictionary<Item, float>();

    public GameObject spinningAxe;
    public GameObject axeIcon;
    public GameObject excalibur;
    public GameObject excaliburIcon;
    public GameObject speedIncreaseIcon;
    public GameObject shieldIcon;
    public GameObject radiusBuffIcon;    

    private void Awake()
    {
        numberOfItem = new Dictionary<Item, float>()
        {
            {Item.Excalibur,0 },
            {Item.SpinningAxe, 0},
            {Item.SuperBlastRadius, 0},
            {Item.Shield, 0},
            {Item.SpeedIncrease,0 },
            {Item.Heal, 0}
        };
    }

    private void Update()
    {
        HandleExcalibur();
        HandleSpinningAxe();

        SpeedIncrease();
        HandleShield();
        HandleBlastRadius();

        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void AddItemTime(int time, Item item)
    {
        numberOfItem[item] += time * affectTimeOfItem;
    }

    public void AddWeaponTime(int time, Item item)
    {
        numberOfItem[item] += time * affectTimeOfWeapon;
    }

    public void SpeedIncrease()
    {
        if (numberOfItem[Item.SpeedIncrease] > 0)
        {
            numberOfItem[Item.SpeedIncrease] -= Time.deltaTime;
            if (speedRealTime == speedInit) speedRealTime = speedInit + moreSpeed;
            this.GetComponent<PlayerMovement>().SpeedChange(speedRealTime);
            if(!speedIncreaseIcon.activeSelf) speedIncreaseIcon.SetActive(true);
            //NotifyObservers(PlayerAction.SpeedIncrease, 0);
        }
        else
        {
            this.GetComponent<PlayerMovement>().SpeedChange(speedInit);
            speedRealTime = speedInit;
            if (speedIncreaseIcon.activeSelf) speedIncreaseIcon.SetActive(false);
        }
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
            GetComponent<PlayerMovement>().Flickering();
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
        if (numberOfItem[Item.Shield] > 0)
        {
            numberOfItem[Item.Shield] -= Time.deltaTime;
            shield = true;
            if (!shieldIcon.activeSelf) shieldIcon.SetActive(true);
        }
        else
        {
            shield = false;
            if (shieldIcon.activeSelf) shieldIcon.SetActive(false);
        }
    }

    public void HandleBlastRadius()
    {
        if (numberOfItem[Item.SuperBlastRadius] > 0)
        {
            numberOfItem[Item.SuperBlastRadius] -= Time.deltaTime;
            if (radiusRealtime == explosionRadiusReal)
            {
                radiusRealtime = radiusRealtime + moreRadius;
                NotifyObservers(PlayerAction.BlastRadius, moreRadius);
                GetComponent<BombController>().RadiusChange(radiusRealtime);
                if (!radiusBuffIcon.activeSelf)
                {
                    radiusBuffIcon.SetActive(true);
                }
            }
        }
        else
        {
            if (radiusBuffIcon.activeSelf)
            {
                radiusBuffIcon.SetActive(false);
            }
            GetComponent<BombController>().RadiusChange(explosionRadiusReal);
            radiusRealtime = explosionRadiusReal;
        }
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
        if (numberOfItem[Item.SpinningAxe] > 0)
        {
            numberOfItem[Item.SpinningAxe] -= Time.deltaTime;
            if (!spinningAxe.activeSelf) spinningAxe.SetActive(true);
            if(!axeIcon.activeSelf) axeIcon.SetActive(true);
        }
        else
        {
            if (spinningAxe.activeSelf) spinningAxe.SetActive(false);
            if (axeIcon.activeSelf) axeIcon.SetActive(false);
        }       
    }

    public void HandleExcalibur()
    {
        if (numberOfItem[Item.Excalibur] > 0)
        {
            if (!excalibur.activeSelf) excalibur.SetActive(true);
            if (!excaliburIcon.activeSelf) excaliburIcon.SetActive(true);
        }
        else
        {
            if (excalibur.activeSelf) excalibur.SetActive(false);
            if (excaliburIcon.activeSelf) excaliburIcon.SetActive(false);
        }
    }

}
