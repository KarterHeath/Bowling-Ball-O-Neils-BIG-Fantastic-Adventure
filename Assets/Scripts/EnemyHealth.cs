using UnityEngine;

public class EnemyHealth : Health
{
    protected override void Die()
    {
        if (dead) return;

        base.Die();

        // Disable enemy scripts
        MeleeEnemy melee = GetComponent<MeleeEnemy>();
        if (melee != null)
            melee.enabled = false;

        EnemyAI ai = GetComponentInParent<EnemyAI>();
        if (ai != null)
            ai.enabled = false;

        // Destroy after animation
        Destroy(gameObject, 1.5f);
    }
}
