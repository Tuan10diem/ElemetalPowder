using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombController : MonoBehaviour
{

    public GameObject bombPrefab;
    public Explosion explosionPrefab;
    public LayerMask layerMask;
    public Tilemap destructibles;
    public Exploding explodingPrefab;
    
    public KeyCode inputKey = KeyCode.Space;

    private int bombAmount = 3;
    public int bombRemaining = 3;
    public float bombFuseTime = 3f;
    
    public int explosionRadius;
    public float explosionDuration = 1f;

    private void Awake()
    {
        explosionRadius = GetComponent<PlayerStatus>().explosionRadiusReal;
    }

    // Update is called once per frame
    void Update()
    {
        if (bombRemaining!=0 && Input.GetKeyDown(inputKey))
        {
            StartCoroutine(PlaceBomb());
            GetComponent<PlayerStatus>().PlaceBomb();
        }
    }

    private IEnumerator PlaceBomb()
    {
        Vector2 pos = this.transform.position;
        pos.x = Mathf.Round(pos.x);
        pos.y = Mathf.Round(pos.y);

        GameObject bomb = Instantiate(bombPrefab, pos, Quaternion.identity);
        bombRemaining--;

        yield return new WaitForSeconds(bombFuseTime);
        Destroy(bomb);
        bombRemaining= Mathf.Min(bombRemaining+1, bombAmount);

        SetupExplode(bomb.transform.position);
        
        Destroy(bomb.gameObject);
        
    }

    private void SetupExplode(Vector2 position)
    {
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveSpriteRenderer(explosion.top);
        explosion.DestroyAfter(explosionDuration);

        Explode(position, Vector2.up, explosionRadius);
        Explode(position, Vector2.down, explosionRadius);
        Explode(position, Vector2.left, explosionRadius);
        Explode(position, Vector2.right, explosionRadius);
    }

    private void Explode(Vector2 position, Vector2 direction, int length)
    {
        if (length <= 0) return;
        
        position += direction;
        
        if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, layerMask))
        {
            ClearDestructible(position);
            return;
        }

        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);

        if (length > 1)
        {
            explosion.SetActiveSpriteRenderer(explosion.middle);
        }
        else
        {
            explosion.SetActiveSpriteRenderer(explosion.bot);
        }
        
        explosion.SetDirection(direction);
        
        explosion.DestroyAfter(explosionDuration);
        
        Explode(position, direction, length - 1);
    }

    private void ClearDestructible(Vector2 position)
    {
        Vector3Int cellPos = destructibles.WorldToCell(position);
        TileBase cell = destructibles.GetTile(cellPos);

        if (cell != null)
        {
            Instantiate(explodingPrefab, position, Quaternion.identity);
            destructibles.SetTile(cellPos,null);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Bomb")) {
            other.isTrigger = false;
        }
    }

    public IEnumerator BlastRadius(int expRadius, int expRadiusAfterBuff, float affectTime)
    {
        this.explosionRadius = expRadiusAfterBuff;
        yield return new WaitForSeconds(affectTime);
        this.explosionRadius = expRadius;
    }
    
}
