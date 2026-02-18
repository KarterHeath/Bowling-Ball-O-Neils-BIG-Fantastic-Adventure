using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    private PlayerMovement playerMovement;

    [Header("Death Sound")]
    [SerializeField] private AudioClip deathSound;

    protected override void Awake()
    {
        base.Awake();
        playerMovement = GetComponent<PlayerMovement>();
    }

    protected override void Die()
    {
        if (dead) return;

        base.Die();

        if (playerMovement != null)
            playerMovement.enabled = false;

        SoundManager.instance?.PlaySound(deathSound);

        // Restart level after short delay
        Invoke(nameof(RestartLevel), 1.5f);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}