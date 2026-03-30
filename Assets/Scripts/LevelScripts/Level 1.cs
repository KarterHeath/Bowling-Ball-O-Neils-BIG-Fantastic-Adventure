using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    private void Start()
    {
        if (MusicManager.Instance != null && SceneManager.GetActiveScene().name == "Level1")
        {
            MusicManager.Instance.PlayMusic("Level1Theme");
        }
    }
}