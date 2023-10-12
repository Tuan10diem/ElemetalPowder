using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item itemType;
    public float destroyAfter = 10f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,destroyAfter);
    }

    private void OnPickupItem(PlayerStatus other)
    {
        switch (itemType)
        {
            case Item.Shield:
                other.HandleShield();
                break;
            case Item.SpeedIncrease:
                other.SpeedIncrease(10f);
                break;
            case Item.HandleBomb:
                break;
            case Item.SuperBlastRadius:
                other.HandleBlastRadius(2);
                break;
            case Item.Heal:
                other.HandleHeal(1);
                break;
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnPickupItem(other.gameObject.GetComponent<PlayerStatus>());
        }
    }
}
