using System.Collections;
using UnityEngine;

public class DelaySound : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        StartCoroutine(PlaySoundAfterDelay(11f));
    }

    IEnumerator PlaySoundAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSource.Play();
    }
}