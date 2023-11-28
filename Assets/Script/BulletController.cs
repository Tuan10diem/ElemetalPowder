using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BulletController : MonoBehaviour
{

    public float speed;
    public int damage;
    public bool needToDestroy;
    private Tilemap destructibles;
    public GameObject explodingPrefab;

    private void Start()
    {
        destructibles = GameObject.Find("Destructible").GetComponent<Tilemap>();
        if(needToDestroy) Destroy(gameObject,10f);
    }

    // Update is called once per frame
    void Update()
    {
        BulletMovement();
        if(destructibles) ClearDestructible(new Vector2((int) this.transform.position.x, (int)this.transform.position.y));
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
        if (needToDestroy) Destroy(gameObject);
    }

    private void ClearDestructible(Vector2 position)
    {
        Vector3Int cellPos = destructibles.WorldToCell(position);
        TileBase cell = destructibles.GetTile(cellPos);

        if (cell != null)
        {
            Instantiate(explodingPrefab, position, Quaternion.identity);
            destructibles.SetTile(cellPos, null);
            if (needToDestroy) Destroy(this.gameObject);
        }
    }
}
