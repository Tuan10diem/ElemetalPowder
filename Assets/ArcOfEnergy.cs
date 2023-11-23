using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcOfEnergy : BulletController
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null || other.CompareTag("Player")) return;
        if (other.CompareTag("Boss"))
        {
            other.GetComponent<BossController>().HandleHurt(damage);
            Destroy(gameObject);
        }
        
    }
}
