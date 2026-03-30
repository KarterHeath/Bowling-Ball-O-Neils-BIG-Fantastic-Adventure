using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel3 : MonoBehaviour
{
    void Start()
    {
        if (MusicManager.Instance != null && SceneManager.GetActiveScene().name == "Level3")
        {
            MusicManager.Instance.PlayMusic("Level3Theme");
        }
    }
    public void LoadLevel()
    {
        MusicManager.Instance.PlayMusic("Level3Theme");
        SceneManager.LoadScene("Level3");

    }
}
