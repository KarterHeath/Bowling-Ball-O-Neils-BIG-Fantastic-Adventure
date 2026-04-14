using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel6 : MonoBehaviour
{
    void Start()
    {
        if (MusicManager.Instance != null && SceneManager.GetActiveScene().name == "Tavern End")
        {
            MusicManager.Instance.PlayMusic("Tavern End");
        }
    }
    public void LoadLevel()
    {
        MusicManager.Instance.PlayMusic("Tavern End");
        SceneManager.LoadScene("Tavern End");

    }
}
