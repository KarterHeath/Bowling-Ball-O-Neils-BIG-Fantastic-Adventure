using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CutsceneToMenu : MonoBehaviour
{
    // The name or build index of your main menu scene
    public string mainMenuSceneName = "Main Menu";
    // The duration of the cutscene in seconds
    public float cutsceneDuration = 76.30f;

    void Start()
    {
        // Start the coroutine to load the menu after a delay
        StartCoroutine(TransitionAfterDelay(cutsceneDuration));
    }

    IEnumerator TransitionAfterDelay(float delay)
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(delay);

        // Load the main menu scene by name or index
        LoadMainMenu();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
