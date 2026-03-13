using System.Collections;
using UnityEngine;
using System.Collections.Generic;


[System.Serializable]
public class PlayerData
{
    public int coins;
  
    public PlayerData(Player player)
    {
        coins = player.coins;
        

       
    }

}
