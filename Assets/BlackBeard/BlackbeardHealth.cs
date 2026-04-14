using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BlackbeardHealth : Health
{
    public AudioClip deathClip;
    private Blackbeard blackbeard;

    protected override void Awake()
    {
        base.Awake();
        blackbeard = GetComponent<Blackbeard>();
    }

    public override void TakeDamage(float damage)
    {
        if (invulnerable || dead) return;

        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetBool("hurt", true);

            StartCoroutine(Invulnerability());
        }
        else
        {
            Die();
        }
    }

    protected override void Die()
    {
        if (dead) return;

        dead = true;

        anim.SetBool("Die", true);

        // Disable AI
        blackbeard.enabled = false;

       

        // destroy after animation
        Destroy(gameObject, 6);
        deathClip = blackbeard.deathClip;
        SceneManager.LoadScene("Ending Cutscene");
        
    }
}