using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel4 : MonoBehaviour
{
    void Start()
    {
        if (MusicManager.Instance != null && SceneManager.GetActiveScene().name == "Level 4")
        {
            MusicManager.Instance.PlayMusic("Level4Theme");
        }
    }
    public void LoadLevel()
    {
        
        MusicManager.Instance.PlayMusic("Level4Theme");
        SceneManager.LoadScene("Level 4");
    }
}
