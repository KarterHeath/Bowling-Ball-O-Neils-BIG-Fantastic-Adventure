using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CutsceneToPostCredit : MonoBehaviour
{
    // The name or build index of your post-credit scene
    public string PostCreditScene = "Post Credit Scene";
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

        // Load the post-credit scene by name or index
        LoadPostCreditScene();
    }

    public void LoadPostCreditScene()
    {
        SceneManager.LoadScene(PostCreditScene);
    }
}
