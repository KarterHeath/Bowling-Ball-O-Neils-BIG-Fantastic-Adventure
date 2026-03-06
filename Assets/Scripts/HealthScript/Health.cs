using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] protected float startingHealth;
    public float currentHealth { get; protected set; }
    protected bool dead;

    [Header("iFrames")]
    [SerializeField] protected float iFramesDuration;
    [SerializeField] protected int numberOfFlashes;

    protected Animator anim;
    protected SpriteRenderer spriteRend;
    protected bool invulnerable;

    protected virtual void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public virtual void TakeDamage(float damage)
    {
        if (invulnerable || dead) return;

        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
        }
        else
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        dead = true;
        anim.SetTrigger("die");
    }

    public void AddHealth(float value)
    {
        currentHealth = Mathf.Clamp(currentHealth + value, 0, startingHealth);
    }

    protected IEnumerator Invulnerability()
    {
        invulnerable = true;

        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }

        invulnerable = false;
    }
}
