using UnityEngine;
using System;
using UnityEngine.AdaptivePerformance;
using UnityEngine.SceneManagement; 
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
     //How much big character
    public float scaleMultiplier;
   public Animator animator;
   bool  IamBig;

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

    private void FixedUpdate()
    {
      animator.SetFloat("xVelocity", Mathf.Abs(playerRigidbody.linearVelocity.x));
    }
  void  GetBig()
     {
       
     Collider2D col = GetComponent<Collider2D>();

     float heightBefore = col.bounds.size.y; //collider height

     transform.localScale *= scaleMultiplier; //x height

     float heightAfter = col.bounds.size.y;  // collider height after
     float heightDifference = heightAfter - heightBefore; //collider fixed

    transform.position += new Vector3(0, heightDifference / 2f, 0); //player moves up to it grows up

     playerRigidbody.mass = 5f; // more heavy so it can MOVE STUFF

      //  FIX GROUND CHECK
      Transform grcheck = transform.Find("GroundCheck");
      grcheck.localPosition = new Vector3(grcheck.localPosition.x,grcheck.localPosition.y / scaleMultiplier,grcheck.localPosition.z);
     }

     void getSmoll()
     {
    Collider2D col = GetComponent<Collider2D>();

     float heightBefore = col.bounds.size.y; ////collider height now
     
     transform.localScale /= scaleMultiplier; // Get Smaller

     float heightAfter = col.bounds.size.y; //Collider heigh now after smaller

     float heightDifference = heightBefore - heightAfter;//Differences

      // Move down so feet stay grounded
     transform.position -= new Vector3(0, heightDifference / 2f, 0);

     // Restore mass
     playerRigidbody.mass = 0.2f;

     //Fix groundChek
     Transform grcheck = transform.Find("GroundCheck");
      grcheck.localPosition = new Vector3(grcheck.localPosition.x,grcheck.localPosition.y * scaleMultiplier,grcheck.localPosition.z);
     }
     
   
   void setMovement()
{
    isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

    float moveInput = 0f;

    if (Input.GetKey(KeyCode.D))
    {
        moveInput = 1f;
        isForwardDirection = true;
    }
    else if (Input.GetKey(KeyCode.A))
    {
        moveInput = -1f;
        isForwardDirection = false;
    }
    else if (Input.GetKey(KeyCode.S))
    {
        if(IamBig == true) 
        {
        IamBig = false;
        getSmoll();
        };
    }

    playerRigidbody.linearVelocity = new Vector2(moveInput * 5f, playerRigidbody.linearVelocity.y);

    spriteRenderer.flipX = !isForwardDirection;

    if (Input.GetKeyDown(KeyCode.W) && isGrounded)
    {
        playerRigidbody.linearVelocity = new Vector2(playerRigidbody.linearVelocity.x, 10f);
    }
}

private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            Destroy(other.gameObject);
            IamBig = true;
            GetBig();
        }
        if(other.CompareTag("JAM"))
        {
              SceneManager.LoadScene("Level2");
        }
        if(other.CompareTag("DEAD"))
        {
              SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
