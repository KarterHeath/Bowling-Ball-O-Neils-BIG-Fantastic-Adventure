using UnityEngine;
using System.Collections;

public class PlayAudioAtTime : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        StartCoroutine(PlayAfterDelay(28f));
    }

    IEnumerator PlayAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSource.Play();
    }
}