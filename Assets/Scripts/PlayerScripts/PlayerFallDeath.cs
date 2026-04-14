using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class PlayerFallDeath : MonoBehaviour
{
    public float deathY = -8f;
    public float restartDelay = 2f;

    private Animator animator;
    private Rigidbody2D rb;
    public bool isDead = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isDead && transform.position.y < deathY)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        SoundManager.Instance.PlaySound2D("Die");

        // Stop movement
        rb.linearVelocity = Vector2.zero;
        

        // Play animation
        animator.SetTrigger("die");

        // Restart after delay
        Invoke(nameof(RestartLevel), restartDelay);
    }

    private void RestartLevel()
    {
     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
