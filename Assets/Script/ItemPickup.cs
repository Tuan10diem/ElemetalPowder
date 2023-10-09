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

    private void OnPickupItem(GameObject other)
    {
        switch (itemType)
        {
            case Item.Shield:
                break;
            case Item.SpeedIncrease:
                other.GetComponent<PlayerMovement>().SpeedIncrease();
                break;
            case Item.HandleBomb:
                break;
            case Item.SuperBlastRadius:
                break;
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnPickupItem(other.gameObject);
        }
    }
}
