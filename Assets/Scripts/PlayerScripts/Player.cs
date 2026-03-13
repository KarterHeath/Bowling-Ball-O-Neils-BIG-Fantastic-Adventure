using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public int coins;
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
        }
        else
        {
            coins = 0;
        }
    }
    void Update()
    {
        // Player logic goes here
    }
}


