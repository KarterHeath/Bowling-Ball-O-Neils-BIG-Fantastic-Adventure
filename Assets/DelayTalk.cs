using System.Collections;
using UnityEngine;

public class DelayTalk : MonoBehaviour
{
    public AudioSource audioSource;

void Start()
{
    StartCoroutine(PlaySoundAfterDelay(5f));
}

    IEnumerator PlaySoundAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSource.Play();
    }
}   