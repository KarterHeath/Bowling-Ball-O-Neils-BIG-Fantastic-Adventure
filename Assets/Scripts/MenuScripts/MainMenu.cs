using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    // Start is called before the first frame update
    public Slider musicSlider, sfxSlider;

    private void Start()
    {
        MusicManager.Instance.PlayMusic("MainTheme");

        if (musicSlider != null)
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0f);
        if (sfxSlider != null)
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0f);
        audioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume", 0f));
        audioMixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume", 0f));
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Levels");
        if (SceneManager.GetActiveScene().name == "Level1")
            MusicManager.Instance.PlayMusic("Level1Theme");
        if (SceneManager.GetActiveScene().name == "Level2")
            MusicManager.Instance.PlayMusic("Level2Theme");
        if (SceneManager.GetActiveScene().name == "Level3")
            MusicManager.Instance.PlayMusic("Level3Theme");
        if (SceneManager.GetActiveScene().name == "Level4")
            MusicManager.Instance.PlayMusic("Level4Theme");
        if (SceneManager.GetActiveScene().name == "Level5")
            MusicManager.Instance.PlayMusic("Level5Theme");
        if (SceneManager.GetActiveScene().name == "Tavern End")
            MusicManager.Instance.PlayMusic("TavernEndTheme");

    }
    // Update is called once per frame
    public void UpdateMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
    public void UpdateSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
}