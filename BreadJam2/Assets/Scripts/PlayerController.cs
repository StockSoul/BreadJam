using UnityEngine;
using System;
using UnityEngine.AdaptivePerformance;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public Rigidbody2D playerRigidbody;
    public Transform groundCheck;
    public Transform roofCheck;
    public Transform shieldTransform;
    public LayerMask groundLayer;
    public LayerMask roofLayer;
    public SpriteRenderer spriteRenderer;

    Vector2 velocity;
    int gravityScale = 1;
    public bool isGrounded;
    bool isRoof;
    bool isForwardDirection;

    [Header("Sounds")]
    public AudioSource jump, dash;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        setMovement();
    }

    void setMovement()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        if (Input.GetKey(KeyCode.D))
        {
            isForwardDirection = true;
            playerRigidbody.linearVelocity = new Vector2(GetHorizontalSpeed(), playerRigidbody.linearVelocity.y);
        }
        if (Input.GetKey(KeyCode.A))
        {
            isForwardDirection = false;
            playerRigidbody.linearVelocity = new Vector2(-GetHorizontalSpeed(), playerRigidbody.linearVelocity.y);
        }
        spriteRenderer.flipX = isForwardDirection;
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            playerRigidbody.linearVelocity = new Vector2(playerRigidbody.linearVelocity.x, 10.0f);
            //jump.Play();
        }
    }
    float GetHorizontalSpeed()
    {
        return Math.Abs(playerRigidbody.linearVelocity.x) > 10.0f ? Math.Abs(playerRigidbody.linearVelocity.x) : 10.0f;
    }
}
