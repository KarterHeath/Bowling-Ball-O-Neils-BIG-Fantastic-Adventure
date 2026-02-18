using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;


    public string playerName = "PlayerName";



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
