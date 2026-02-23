using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public void LoadLevel1()
    {
        MusicManager.Instance.PlayMusic("Level1Theme");
        SceneManager.LoadScene("Level 1");
       
    }
}
