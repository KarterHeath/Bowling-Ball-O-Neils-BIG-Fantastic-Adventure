using UnityEngine;
using System.Collections;

public class PlayAudioOnApproach : MonoBehaviour
{
    [Header("Settings")]
    public Transform player;
    public float triggerDistance = 3f;
    public bool playOnce = true;

    [Header("Dialogue Audio")]
    public AudioSource dialogueSource;

    [Header("Background Music")]
    public AudioSource musicSource;
    public float fadedMusicVolume = 0.2f;
    public float fadeDuration = 1f;

    [Header("Animation")]
    public Animator characterAnimator;
    public string talkingBoolName = "Talking";

    private bool hasPlayed = false;
    private bool isDialoguePlaying = false;
    private float originalMusicVolume;

    void Start()
    {
        if (musicSource != null)
            originalMusicVolume = musicSource.volume;
    }

    void Update()
    {
        if (player == null || dialogueSource == null) return;

        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= triggerDistance)
        {
            if (!dialogueSource.isPlaying && (!playOnce || !hasPlayed))
            {
                PlayDialogue();
            }
        }

        if (isDialoguePlaying && !dialogueSource.isPlaying)
        {
            StopDialogue();
        }
    }

    void PlayDialogue()
    {
        dialogueSource.Play();
        hasPlayed = true;
        isDialoguePlaying = true;

        if (characterAnimator != null)
            characterAnimator.SetBool(talkingBoolName, true);

        if (musicSource != null)
            StartCoroutine(FadeMusic(originalMusicVolume, fadedMusicVolume));
    }

    void StopDialogue()
    {
        isDialoguePlaying = false;

        if (characterAnimator != null)
            characterAnimator.SetBool(talkingBoolName, false);

        if (musicSource != null)
            StartCoroutine(FadeMusic(fadedMusicVolume, originalMusicVolume));
    }

    IEnumerator FadeMusic(float startVolume, float targetVolume)
    {
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, targetVolume, time / fadeDuration);
            yield return null;
        }

        musicSource.volume = targetVolume;
    }
}
