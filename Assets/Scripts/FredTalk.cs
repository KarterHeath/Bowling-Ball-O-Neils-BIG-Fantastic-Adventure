using UnityEngine;

public class PlayAudioOnApproach : MonoBehaviour
{
    [Header("Settings")]
    public Transform player;
    public float triggerDistance = 3f;
    public bool playOnce = true;

    [Header("Audio")]
    public AudioSource audioSource;

    [Header("Animation")]
    public Animator characterAnimator;   // Drag your Animator here
    public string animationTriggerName = "Talk"; // Name of trigger in Animator

    private bool hasPlayed = false;

    void Update()
    {
        if (player == null || audioSource == null) return;

        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= triggerDistance)
        {
            if (!audioSource.isPlaying && (!playOnce || !hasPlayed))
            {
                audioSource.Play();
                hasPlayed = true;

                // Trigger animation when audio starts
                if (characterAnimator != null && !string.IsNullOrEmpty(animationTriggerName))
                {
                    characterAnimator.SetTrigger(animationTriggerName);
                }
            }
        }
    }
}
