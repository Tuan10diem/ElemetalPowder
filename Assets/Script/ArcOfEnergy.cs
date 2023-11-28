using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcOfEnergy : BulletController
{

    private void Start()
    {
        Destroy(gameObject,15f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other) return;
        if (other.CompareTag("Boss"))
        {
            other.GetComponent<BossController>().HandleHurt(damage);
            Destroy(gameObject);
        }
    }
}
