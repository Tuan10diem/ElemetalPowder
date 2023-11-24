using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private float speed;
    public float pressX = 0f;
    public float pressY = 0f;
    public Vector2 direction = Vector2.down;

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

    [Header("Input")]
    public float flickerSpeed = 10f;
    public Color damageColor = Color.red;
    private Color originalColor;
    private bool isFlickering = false;

    void Awake()
    {
        speed = GetComponent<PlayerStatus>().speedInit;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        activeSpriteRenderer = spriteRendererDown;
        originalColor = Color.white;
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

    void UpdatePosition()
    {
        Vector2 position = _rigidbody2D.position;

        _rigidbody2D.MovePosition(position + direction * speed * Time.fixedDeltaTime);
    }

    public void SpeedChange(float speed)
    {
        this.speed = speed;
    }

    public void Flickering()
    {
        if (!isFlickering)
        {
            // Start the flickering coroutine
            StartCoroutine(FlickerCoroutine());
        }
    }

    private IEnumerator FlickerCoroutine()
    {
        isFlickering = true;

        SpriteRenderer objectRenderer1 = spriteRendererUp.GetComponent<SpriteRenderer>();
        SpriteRenderer objectRenderer2 = spriteRendererDown.GetComponent<SpriteRenderer>();
        SpriteRenderer objectRenderer3 = spriteRendererLeft.GetComponent<SpriteRenderer>();
        SpriteRenderer objectRenderer4 = spriteRendererRight.GetComponent<SpriteRenderer>();

        for(int i=0;i<5;i++)
        {
            objectRenderer1.color = (objectRenderer1.color == originalColor) ? damageColor : originalColor;
            objectRenderer2.color = (objectRenderer2.color == originalColor) ? damageColor : originalColor;
            objectRenderer3.color = (objectRenderer3.color == originalColor) ? damageColor : originalColor;
            objectRenderer4.color = (objectRenderer4.color == originalColor) ? damageColor : originalColor;

            yield return new WaitForSeconds(1f / flickerSpeed);

            objectRenderer1.color = (objectRenderer1.color == originalColor) ? damageColor : originalColor;
            objectRenderer2.color = (objectRenderer2.color == originalColor) ? damageColor : originalColor;
            objectRenderer3.color = (objectRenderer3.color == originalColor) ? damageColor : originalColor;
            objectRenderer4.color = (objectRenderer4.color == originalColor) ? damageColor : originalColor;

            yield return new WaitForSeconds(1f / flickerSpeed);

        }

        objectRenderer1.color = originalColor;
        objectRenderer2.color = originalColor;
        objectRenderer3.color = originalColor;
        objectRenderer4.color = originalColor;

        isFlickering = false;
    }
}