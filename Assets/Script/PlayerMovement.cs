using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Subjects
{
    private Rigidbody2D _rigidbody2D;
    protected float speed = 5f;
    public float pressX = 0f;
    public float pressY = 0f;
    private Vector2 direction = Vector2.down;

    [Header("Input")] 
    public KeyCode inputUp = KeyCode.W;
    public KeyCode inputDown = KeyCode.S;
    public KeyCode inputLeft = KeyCode.A;
    public KeyCode inputRight = KeyCode.D;
    
    [Header("Sprites")]
    public AnimatedSpriteRenderer spriteRendererUp;
    public AnimatedSpriteRenderer spriteRendererDown;
    public AnimatedSpriteRenderer spriteRendererLeft;
    public AnimatedSpriteRenderer spriteRendererRight;
    // public AnimatedSpriteRenderer spriteRendererDeath;
    private AnimatedSpriteRenderer activeSpriteRenderer;

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        activeSpriteRenderer = spriteRendererDown;
    }

    void Update()
    {
        GetInput1D();
    }

    void FixedUpdate()
    {
        UpdatePosition();
    }

    void GetInput1D()
    {
        if (Input.GetKey(inputUp))
        {
            SetDirection(Vector2.up, spriteRendererUp);
        }
        else if (Input.GetKey(inputDown))
        {
            SetDirection(Vector2.down, spriteRendererDown);
        }
        else if (Input.GetKey(inputLeft))
        {
            SetDirection(Vector2.left,spriteRendererLeft);
        }
        else if (Input.GetKey(inputRight))
        {
            SetDirection(Vector2.right,spriteRendererRight);
        }
        else
        {
            SetDirection(Vector2.zero,activeSpriteRenderer);
        }
    }
    
    private void SetDirection(Vector2 newDirection, AnimatedSpriteRenderer spriteRenderer)
    {
        direction = newDirection;
    
        spriteRendererUp.enabled = spriteRenderer == spriteRendererUp;
        spriteRendererDown.enabled = spriteRenderer == spriteRendererDown;
        spriteRendererLeft.enabled = spriteRenderer == spriteRendererLeft;
        spriteRendererRight.enabled = spriteRenderer == spriteRendererRight;
    
        activeSpriteRenderer = spriteRenderer;
        activeSpriteRenderer.idle = direction == Vector2.zero;
    }
    
    void GetInput2D()
    {
        pressX = Input.GetAxis("Horizontal");
        pressY = Input.GetAxis("Vertical");
    }

    void UpdatePosition()
    {
        Vector2 position = _rigidbody2D.position;

        _rigidbody2D.MovePosition(position + direction * speed * Time.fixedDeltaTime);
    }

    public void SpeedIncrease()
    {
        speed++;
        NotifyObservers(PlayerAction.SpeedIncrease);
    }
}