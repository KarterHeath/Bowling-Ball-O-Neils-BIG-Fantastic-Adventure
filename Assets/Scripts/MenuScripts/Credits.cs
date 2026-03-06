using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsButton : MonoBehaviour
{
    // Call this function from your Credits button in the Inspector
    public void GoToCredits()
    {
        // Make sure your Credits scene is added in Build Settings (File > Build Settings)
        SceneManager.LoadScene("Credits");
    }
}
