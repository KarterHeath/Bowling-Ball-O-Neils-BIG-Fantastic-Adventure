using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level 1");
    }
}
