using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CutsceneToEnd : MonoBehaviour
{
    // The name or build index of your end scene
    public string EndSceneName = "Tavern End";
    // The duration of the cutscene in seconds
    public float cutsceneDuration = 11f;

    void Start()
    {
        // Start the coroutine to load the menu after a delay
        StartCoroutine(TransitionAfterDelay(cutsceneDuration));
    }

    IEnumerator TransitionAfterDelay(float delay)
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(delay);

        // Load the end scene by name or index
        LoadEndScene();
    }

    public void LoadEndScene()
    {
        SceneManager.LoadScene(EndSceneName);
    }
}
