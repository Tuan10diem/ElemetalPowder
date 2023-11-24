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

    public void OnPickupItem(PlayerStatus other)
    {
        switch (itemType)
        {
            case Item.Shield:
                other.AddItemTime(1, Item.Shield);
                break;
            case Item.SpeedIncrease:
                other.AddItemTime(1, Item.SpeedIncrease);
                break;
            case Item.SuperBlastRadius:
                other.AddItemTime(1, Item.SuperBlastRadius);
                break;
            case Item.Heal:
                other.numberOfItem[Item.Heal]++;
                break;
            case Item.SpinningAxe:
                other.AddWeaponTime(1, Item.SpinningAxe);
                break;
            case Item.Excalibur:
                other.numberOfItem[Item.Excalibur]++;
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
