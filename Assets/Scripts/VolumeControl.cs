using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public AudioSource musicSource;
    public Slider volumeSlider;

    void Start()
    {
        volumeSlider.value = musicSource.volume;
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    void SetVolume(float volume)
    {
        musicSource.volume = volume;
    }
}
