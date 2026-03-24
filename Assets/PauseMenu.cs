using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PauseMenu : MonoBehaviour
// This script is responsible for handling the pause menu functionality, including pausing the game, resuming the game, and navigating back to the main menu.
{
    [SerializeField] GameObject pauseMenu;
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0; // Pause the game by setting time scale to 0
    }
    public void Home()
    {
        SceneManager.LoadScene("Main Menu");
        MusicManager.Instance.PlayMusic("MainTheme");
        Time.timeScale = 1; // Ensure the game is resumed when going back to the main menu
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1; // Resume the game by setting time scale back to 1
    }
}