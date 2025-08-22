using UnityEngine;

public class EnemySight : MonoBehaviour
{
    // Reference to the player
    public Transform player;

    // Enemy movement speeds
    public float patrolSpeed = 2f;
    public float chaseSpeed = 3.5f;

    // Vision settings
    public float visionRange = 5f;
    public float stopDistance = 1f;

    // Patrol settings
    public Transform pointA;   // First patrol point
    public Transform pointB;   // Second patrol point
    private Transform currentTarget;

    // Tracking player detection
    private bool playerNoticed = false;

    void Start()
    {
        // Start patrolling between pointA and pointB
        currentTarget = pointA;
    }

    void Update()
    {
        // Check distance between enemy and player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // If player is within vision range, notice them
        if (distanceToPlayer <= visionRange)
        {
            playerNoticed = true;
        }
        else
        {
            playerNoticed = false;
        }

        // Decide behavior: chase OR patrol
        if (playerNoticed)
        {
            ChasePlayer(distanceToPlayer);
        }
        else
        {
            Patrol();
        }
    }

    // Patrols between point A and point B
    void Patrol()
    {
        Vector2 direction = (currentTarget.position - transform.position).normalized;

        // Move toward the current patrol target
        transform.position += (Vector3)direction * patrolSpeed * Time.deltaTime;

        // Flip to face the player (instead of patrol point)
        FacePlayer();

        // If close enough to target, switch patrol point
        if (Vector2.Distance(transform.position, currentTarget.position) < 0.2f)
        {
            currentTarget = (currentTarget == pointA) ? pointB : pointA;
        }
    }

    // Chases the player until within stopping distance
    void ChasePlayer(float distanceToPlayer)
    {
        Vector2 direction = (player.position - transform.position).normalized;

        if (distanceToPlayer > stopDistance)
        {
            transform.position += (Vector3)direction * chaseSpeed * Time.deltaTime;
        }

        // Always face the player while chasing
        FacePlayer();
    }

    // Makes enemy face the player's position
    void FacePlayer()
    {
        if (player != null)
        {
            Vector3 localScale = transform.localScale;
            if (player.position.x > transform.position.x)
                localScale.x = Mathf.Abs(localScale.x); // face right
            else
                localScale.x = -Mathf.Abs(localScale.x); // face left
            transform.localScale = localScale;
        }
    }

    // Draw vision range in the Scene view for debugging
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, visionRange);

        // Draw patrol path
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(pointA.position, pointB.position);
        }
    }
}
