[System.Serializable]
public class SaveData
{

public string playerName;

public SaveData(Player player)
    {
        playerName = player.playerName;
    }
}
