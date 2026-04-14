using System.Collections;
using UnityEngine;

public class Blackbeard : MonoBehaviour
{
    public enum BossState { Patrol, Chase, Attack }
    public BossState currentState = BossState.Patrol;

    [Header("Movement")]
    public Transform[] patrolPoints;
    public float speed = 2f;
    private int destPoint = 0;

    [Header("Jump")]
    public float jumpForce = 7f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    private bool isGrounded;

    [Header("Auto Jump")]
    public float jumpInterval = 5f;
    private float nextJumpTime;

    [Header("Player Detection")]
    public Transform player;
    public float detectionRange = 6f;

    [Header("Attack")]
    public float attackRange = 1.5f;
    public float attackRate = 2f;
    public int attackDamage = 1;
    private float nextAttackTime;
    public Transform attackPoint;
    public LayerMask playerLayer;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip introClip;
    public AudioClip deathClip;
    public AudioClip attackClip;
    private bool introPlayed = false;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isDead;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        nextJumpTime = Time.time + jumpInterval;
    }

    void Update()
    {
        if (isDead || player == null) return;

        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Automatic jump every 5 seconds
        if (Time.time >= nextJumpTime && isGrounded)
        {
            Jump();
            nextJumpTime = Time.time + jumpInterval;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Play intro sound once
        if (!introPlayed && distanceToPlayer <= detectionRange)
        {
            introPlayed = true;
            audioSource.PlayOneShot(introClip);
        }

        // State logic
        if (distanceToPlayer <= attackRange)
            currentState = BossState.Attack;
        else if (distanceToPlayer <= detectionRange)
            currentState = BossState.Chase;
        else
            currentState = BossState.Patrol;

        switch (currentState)
        {
            case BossState.Patrol:
                PatrolLogic();
                break;

            case BossState.Chase:
                ChasePlayer();
                break;

            case BossState.Attack:
                TryAttack();
                break;
        }
    }

    void PatrolLogic()
    {
        animator.SetBool("isAttacking", false);

        if (patrolPoints.Length == 0) return;

        Vector2 target = patrolPoints[destPoint].position;
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target) < 1f)
        {
            destPoint = (destPoint + 1) % patrolPoints.Length;
            Flip();
        }
    }

    void ChasePlayer()
    {
        animator.SetBool("isAttacking", false);

        Vector2 direction = (player.position - transform.position).normalized;

        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            speed * Time.deltaTime
        );

        if ((direction.x > 0 && transform.localScale.x > 0) ||
            (direction.x < 0 && transform.localScale.x < 0))
        {
            Flip();
        }
    }

    void TryAttack()
    {
        if (Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + attackRate;
            StartCoroutine(AttackRoutine());
        }
    }

    IEnumerator AttackRoutine()
    {
        animator.SetBool("isAttacking", true);

        yield return new WaitForSeconds(0.4f);

        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRange,
            playerLayer
        );

        foreach (Collider2D hit in hitPlayers)
        {
            PlayerHealth ph = hit.GetComponent<PlayerHealth>();
            if (ph != null)
                ph.TakeDamage(attackDamage);
        }

        yield return new WaitForSeconds(0.3f);
        animator.SetBool("isAttacking", false);
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        animator.SetTrigger("Jump");
    }

    public void TakeDamage(int damage)
    {
    }

    void Die()
    {
        if (isDead) return;

        isDead = true;

        animator.SetBool("Die", true);

        if (deathClip != null)
            audioSource.PlayOneShot(deathClip);

        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;

        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;

        StopAllCoroutines();

        StartCoroutine(DestroyAfterDeath());
    }

    IEnumerator DestroyAfterDeath()
    {
        if (deathClip != null)
            yield return new WaitForSeconds(deathClip.length);
        else
            yield return new WaitForSeconds(1f);

        Destroy(gameObject);
    }

    void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnDrawGizmos()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}