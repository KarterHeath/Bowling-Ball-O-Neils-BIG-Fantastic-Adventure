using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    // Start is called before the first frame update
    private void Start()
    {
        MusicManager.Instance.PlayMusic("MainTheme");
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Levels");
       

    }
    // Update is called once per frame
    public void UpdateMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
    }
    public void UpdateSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
    }
}