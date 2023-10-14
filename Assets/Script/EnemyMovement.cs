using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class EnemyMovement : MonoBehaviour
{
    public Rigidbody2D _rigidbody2D;

    private List<Vector2> directionList;

    private float timePerUpdateDirection = 0.5f;
    private float timer = 2f;
    private RaycastHit2D lineSign;
    
    private float maxDistanceRaycast = 0.6f;
    private int layerMask = ~(1 << 7);

    public Vector2 direction = Vector2.up;
    private float speed;
    
    private List<Vector2> fordable = new List<Vector2>();

    [Header("Sprites")] 
    public AnimatedSpriteRenderer spriteRendererUp;
    public AnimatedSpriteRenderer spriteRendererDown;
    public AnimatedSpriteRenderer spriteRendererLeft;
    public AnimatedSpriteRenderer spriteRendererRight;
    // public AnimatedSpriteRenderer spriteRendererDeath;
    private AnimatedSpriteRenderer activeSpriteRenderer;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        activeSpriteRenderer = spriteRendererDown;

        speed = GetComponent<EnemyStatus>().speedInit;

    }

    private void Update()
    {
        lineSign = Physics2D.Raycast(transform.position, direction, 10, layerMask);
        
        UpdatePosition();
        UpdateDirection();
    }

    private bool isNeedToUpdate()
    {
        Vector2 pos = this.transform.position;
        pos.x = Mathf.Round(pos.x);
        pos.y = Mathf.Round(pos.y);
        if (Physics2D.Raycast(transform.position, direction, maxDistanceRaycast, ~(1 << 7)).collider != null)
        {
            return true;
        }
        // Debug.Log(pos);
        // Debug.Log(Vector2.Distance(this.transform.position, pos));

        if (Vector2.Distance(this.transform.position, pos) <= 0.1f)
        {
            return true;
        }

        return false;
    }

    private void UpdateDirection()
    {
        timer += Time.deltaTime;
        if (isNeedToUpdate() && timer>timePerUpdateDirection && !isChasing())
        {
            // Debug.Log("Need to update");
            
            fordable = new List<Vector2>();

            if (Physics2D.Raycast(transform.position, Vector2.up, maxDistanceRaycast, layerMask).collider == null)
            {
                fordable.Add(Vector2.up);
            }
            if (Physics2D.Raycast(transform.position, Vector2.down, maxDistanceRaycast, layerMask).collider == null)
            { 
                fordable.Add(Vector2.down);
            }
            if (Physics2D.Raycast(transform.position, Vector2.left, maxDistanceRaycast, layerMask).collider == null)
            {
                fordable.Add(Vector2.left);
            }
            if (Physics2D.Raycast(transform.position, Vector2.right, maxDistanceRaycast, layerMask).collider == null)
            {
                fordable.Add( Vector2.right);
            }
            
            if (Physics2D.Raycast(transform.position, direction, maxDistanceRaycast, layerMask).collider == null)
            {
                fordable.Add(direction);
                fordable.Add(direction);
                fordable.Add(direction);
                fordable.Add(direction);
                fordable.Add(direction);
                fordable.Add(direction);
            }


            if (fordable.Count > 0)
            {
                timer = 0;
                int randomDir = Random.Range(0, fordable.Count);
                direction = fordable[randomDir];
                if (direction == Vector2.up)
                    SetDirectionSpriteRenderer(spriteRendererUp);
                else if (direction == Vector2.down)
                    SetDirectionSpriteRenderer(spriteRendererDown);
                else if (direction == Vector2.left)
                    SetDirectionSpriteRenderer(spriteRendererLeft);
                else if (direction == Vector2.right)
                    SetDirectionSpriteRenderer(spriteRendererRight);
                
            }
        }
    }

    public bool isChasing()
    {
        if (lineSign.collider == null) return false;
        if (lineSign.collider.CompareTag("Player"))
        {
            return true;
        }
        return false;
    }

    private void UpdatePosition()
    {
        Vector2 pos = _rigidbody2D.transform.position;
        _rigidbody2D.MovePosition(pos + speed * direction * Time.deltaTime);
    }

    private void SetDirectionSpriteRenderer(AnimatedSpriteRenderer spriteRenderer)
    {
        spriteRendererUp.enabled = spriteRenderer == spriteRendererUp;
        spriteRendererDown.enabled = spriteRenderer == spriteRendererDown;
        spriteRendererLeft.enabled = spriteRenderer == spriteRendererLeft;
        spriteRendererRight.enabled = spriteRenderer == spriteRendererRight;

        activeSpriteRenderer = spriteRenderer;
        activeSpriteRenderer.idle = direction == Vector2.zero;
    }
    
}

