using UnityEngine;
using UnityEngine.UI;

public class MasterVolumeListener : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        volumeSlider.value = AudioListener.volume;
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    void SetVolume(float value)
    {
        AudioListener.volume = value;
    }
}
