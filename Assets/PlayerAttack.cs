using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // How long the player has to wait before attacking again
    [SerializeField] private float attackCooldown;

    // The point where projectiles (fireballs) will spawn from
    [SerializeField] private Transform firePoint;

    // Array of fireball objects to reuse (object pooling system)
    [SerializeField] private GameObject[] fireballs;

    // Reference to Animator to trigger attack animations
    private Animator anim;

    // Reference to PlayerMovement script to check if attacking is allowed
    private PlayerMovement playerMovement;

    // Timer to keep track of attack cooldown
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        // Get references to Animator and PlayerMovement components on the same GameObject
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        // Check for left mouse button click, if attack cooldown has passed, 
        // and if player is allowed to attack
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())

            Attack();

        // Increase cooldown timer with the time passed since last frame
        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        // Trigger the attack animation
        anim.SetTrigger("attack");

        // Reset cooldown timer after attacking
        cooldownTimer = 0;

        // Position a fireball at the firePoint
        fireballs[FindFireball()].transform.position = firePoint.position;

        // Set fireball direction based on player's facing direction
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireball()
    {
        // Loop through fireballs to find one that is inactive in the hierarchy
        // (so we can reuse it instead of instantiating a new one)
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i; // Return index of available fireball
        }

        // If all fireballs are active, default to first one in the array
        return 0;
    }
}
