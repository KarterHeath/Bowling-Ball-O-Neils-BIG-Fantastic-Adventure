using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Call this function when the Settings button is pressed
    public void GoToSettings()
    {
        // Replace "Settings" with the exact name of your Settings scene
        SceneManager.LoadScene("Settings");
    }
}
