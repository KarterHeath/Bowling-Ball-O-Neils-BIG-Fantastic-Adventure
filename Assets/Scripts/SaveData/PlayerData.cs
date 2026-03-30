using System.Collections;
using UnityEngine;
using System.Collections.Generic;


[System.Serializable]
public class PlayerData
{
    public int coins;
    public int unlockedevel;
     public int level
    {
        get { return unlockedevel; }
        set { unlockedevel = value; }
    }

    public PlayerData(Player player)
    {
        coins = player.coins;
        level = player.unlockedLevel;
    }

}
