using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel5 : MonoBehaviour
{
    void Start()
    {
        if (MusicManager.Instance != null && SceneManager.GetActiveScene().name == "Level 5")
        {
            MusicManager.Instance.PlayMusic("Level5Theme");
        }
    }
    public void LoadLevel()
    {
        MusicManager.Instance.PlayMusic("Level5Theme");
        SceneManager.LoadScene("Level 5");
    
    }
}
