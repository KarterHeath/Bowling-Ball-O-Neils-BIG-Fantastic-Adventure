using UnityEngine;

public class PlayAudioOnApproach : MonoBehaviour
{
    [Header("Settings")]
    public Transform player;            // Drag the player here
    public float triggerDistance = 3f;  // Distance to trigger audio
    public bool playOnce = true;        // Should the audio play only once?

    [Header("Audio")]
    public AudioSource audioSource;     // Drag an AudioSource component here

    private bool hasPlayed = false;

    void Update()
    {
        if (player == null || audioSource == null) return;

        // Calculate distance from player to this object
        float distance = Vector3.Distance(player.position, transform.position);

        // If close enough, play audio
        if (distance <= triggerDistance)
        {
            if (!audioSource.isPlaying && (!playOnce || !hasPlayed))
            {
                audioSource.Play();
                hasPlayed = true;
            }
        }
    }
}