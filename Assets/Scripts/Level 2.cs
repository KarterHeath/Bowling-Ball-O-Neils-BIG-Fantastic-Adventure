using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel2 : MonoBehaviour
{

    void Start()
    {
        if (MusicManager.Instance != null && SceneManager.GetActiveScene().name == "Level 2")
        {
            MusicManager.Instance.PlayMusic("Level2Theme");
        }
    }
    public void LoadLevel()
    {
        MusicManager.Instance.PlayMusic("Level2Theme");
        SceneManager.LoadScene("Level 2");
          
    }
}
