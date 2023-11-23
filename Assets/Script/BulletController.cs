using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public float speed;
    public int damage;

    private void Start()
    {
        Destroy(gameObject,10f);
    }

    // Update is called once per frame
    void Update()
    {
        BulletMovement();
    }

    private void BulletMovement()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other==null) return;
        Debug.Log(other.name);
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStatus>().HandleHurt(damage);
        }
        if (other.CompareTag("Boss"))
        {
            other.GetComponent<BossController>().HandleHurt(damage);
        }
        Destroy(gameObject);
    }
}
