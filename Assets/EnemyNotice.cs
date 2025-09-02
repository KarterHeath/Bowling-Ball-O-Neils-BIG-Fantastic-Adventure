using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // Patrol points
    public Transform pointA;
    public Transform pointB;

    // Reference to the player
    public Transform player;

    // Movement settings
    public float speed = 2f;
    public float chaseSpeed = 3f;
    public float detectionRange = 5f;

    // Private variables
    private Vector3 targetPoint;
    private bool chasing = false;
    private bool facingRight = true; // Keep track of enemy's facing direction

    void Start()
    {
        // Start by going to point A
        targetPoint = pointA.position;
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // If player is close enough, start chasing
        if (distanceToPlayer <= detectionRange)
        {
            chasing = true;
        }
        else if (chasing && distanceToPlayer > detectionRange + 1f)
        {
            // If player gets far away, stop chasing and return to patrol
            chasing = false;
            targetPoint = pointA.position; // Reset patrol
        }

        if (chasing)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        // Move towards the patrol target
        transform.position = Vector2.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);

        // Switch target point when reached
        if (Vector2.Distance(transform.position, targetPoint) < 0.2f)
        {
            if (targetPoint == pointA.position)
                targetPoint = pointB.position;
            else
                targetPoint = pointA.position;
        }

        FaceTarget(targetPoint);
    }

    void ChasePlayer()
    {
        // Move toward the player
        transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
        FaceTarget(player.position);
    }

    void FaceTarget(Vector3 target)
    {
        // If target is to the right and enemy not facing right, flip
        if (target.x > transform.position.x && !facingRight)
        {
            Flip();
        }
        // If target is to the left and enemy is facing right, flip
        else if (target.x < transform.position.x && facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Just flip X
        transform.localScale = scale;
    }
}
