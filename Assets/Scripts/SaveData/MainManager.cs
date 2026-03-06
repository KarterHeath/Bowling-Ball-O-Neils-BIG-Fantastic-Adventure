using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Start is called before the first frame update
public class MainManager : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        try
        {
            LoadPlayerData();
        }
        catch (Exception e)
        {
            Debug.LogError(e);
    
        }
    }
    // Update is called once per frame
    public void SavePlayerData()
    {
        SaveSystem.SavePlayer(Player.Instance);
    }

    public void LoadPlayerData()
    {
        SaveData data = SaveSystem.LoadPlayer();

        if (data != null)
        {
            player.playerName = data.playerName;
        }
    }
}
