using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int coins;
    public int level;
    public static Player Instance { get; private set; }

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

    void Start()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            coins = data.coins;
            level = data.level;
        }
        else
        {
            coins = 0;
            level = 1;
        }

    } 
    public void SavePlayerData()
    {
        SaveSystem.SavePlayer(this);
    }
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        level = data.level;
    }
}


