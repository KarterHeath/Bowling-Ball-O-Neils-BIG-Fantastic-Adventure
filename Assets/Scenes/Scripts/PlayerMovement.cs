using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float wallJumpForce = 8f;
    public float wallSlideSpeed = 2f;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    public Transform groundCheck;
    public Transform wallCheck;

    private Animator anim;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isWallSliding;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (rb == null)
            Debug.LogError("Rigidbody2D component not found on this GameObject!");

        if (groundCheck == null || wallCheck == null)
            Debug.LogError("groundCheck or wallCheck Transform not assigned!");

        if (spriteRenderer == null)
            Debug.LogError("SpriteRenderer component not found on this GameObject!");
    }

    void Update()
    {
        // Check if the player is grounded or touching a wall
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        isTouchingWall = Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);

        float horizontalInput = Input.GetAxisRaw("Horizontal");

        // Wall sliding logic
        if (isTouchingWall && !isGrounded && rb.velocity.y < 0)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }

        // Running animation
        anim.SetBool("run", horizontalInput != 0);

        // Move the player
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        // Jump logic (only allows jumping if grounded or touching a wall)
        if ((isGrounded || isTouchingWall) && Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                // Normal jump
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            else if (isTouchingWall)
            {
                // Wall jump
                float wallJumpDirection = (transform.position.x < wallCheck.position.x) ? -1 : 1;
                rb.velocity = new Vector2(wallJumpDirection * wallJumpForce, jumpForce);
            }
        }

        // Flip character sprite
        if (horizontalInput > 0)
            spriteRenderer.flipX = false;
        else if (horizontalInput < 0)
            spriteRenderer.flipX = true;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, 0.2f);
        }
        if (wallCheck != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(wallCheck.position, 0.2f);
        }
    }
}
