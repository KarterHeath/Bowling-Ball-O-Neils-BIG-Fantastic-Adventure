using UnityEngine;

public class SoldierPlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float wallJumpForce = 8f;
    public float wallSlideSpeed = 2f;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    public Transform groundCheck;
    public Transform wallCheck;

    [Header("Shooting Settings")]
    public KeyCode shootKey = KeyCode.T;
    public GameObject bullet;
    public GameObject gunPoint;
    public float bulletStrength = 200f;
    public float maxBulletStrength = 500f;

    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private bool isGrounded;
    private bool isTouchingWall;
    private bool isWallSliding;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (rb == null) Debug.LogError("Rigidbody2D missing!");
        if (anim == null) Debug.LogError("Animator missing!");
        if (spriteRenderer == null) Debug.LogError("SpriteRenderer missing!");
        if (groundCheck == null || wallCheck == null) Debug.LogError("GroundCheck/WallCheck not assigned!");
    }

    void Update()
    {
        HandleMovement();
        HandleShooting();
    }

    private void HandleMovement()
    {
        // Check ground & wall
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        isTouchingWall = Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);

        float horizontalInput = Input.GetAxisRaw("Horizontal");

        // Wall slide
        if (isTouchingWall && !isGrounded && rb.velocity.y < 0)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }

        // Animations
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("touchingGround", isGrounded);

        // Move
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        // Jump
        if ((isGrounded || isTouchingWall) && Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            else if (isTouchingWall)
            {
                float wallJumpDirection = (transform.position.x < wallCheck.position.x) ? -1 : 1;
                rb.velocity = new Vector2(wallJumpDirection * wallJumpForce, jumpForce);
            }
        }

        // Flip sprite
        if (horizontalInput > 0) spriteRenderer.flipX = false;
        else if (horizontalInput < 0) spriteRenderer.flipX = true;
    }

    private void HandleShooting()
    {
        if (Input.GetKey(shootKey))
        {
            bulletStrength += Time.deltaTime * 200f;
        }

        if (Input.GetKeyUp(shootKey))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        bulletStrength = Mathf.Min(bulletStrength, maxBulletStrength);

        GameObject soldierBullet = Instantiate(bullet, gunPoint.transform.position, gunPoint.transform.rotation);
        Rigidbody2D bulletRb = soldierBullet.GetComponent<Rigidbody2D>();

        // Flip bullet direction depending on character facing
        Vector2 shootDir = spriteRenderer.flipX ? Vector2.left : Vector2.right;
        bulletRb.AddForce(shootDir * bulletStrength);

        Destroy(soldierBullet, 1.2f);
        bulletStrength = 200f; // reset
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
