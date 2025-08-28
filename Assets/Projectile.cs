using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Speed at which the projectile moves
    [SerializeField] private float speed;

    // Stores the direction (left or right) the projectile will travel
    private float direction;

    // Tracks if the projectile has already hit something
    private bool hit;

    // Tracks how long the projectile has been active
    private float lifetime;

    // References for animation and collision
    private Animator anim;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        // Get Animator and BoxCollider2D components from this GameObject
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        // If projectile already hit something, stop moving
        if (hit) return;

        // Calculate projectile movement based on speed, frame time, and direction
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        // Track how long projectile has been active
        lifetime += Time.deltaTime;

        // If active for more than 5 seconds, deactivate (prevents infinite projectiles flying offscreen)
        if (lifetime > 5) gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // When projectile hits something:
        hit = true;                        // Mark as hit
        boxCollider.enabled = false;       // Disable collider so it doesn't hit multiple times
        anim.SetTrigger("explode");        // Play explosion animation
    }

    public void SetDirection(float _direction)
    {
        // Reset lifetime whenever projectile is fired
        lifetime = 0;

        // Set the direction projectile will move in
        direction = _direction;

        // Reactivate projectile and reset state
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        // Flip projectile sprite if direction is opposite of current facing
        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        // Deactivates the projectile (called at end of animation event)
        gameObject.SetActive(false);
    }
}
