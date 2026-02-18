using UnityEngine;

public class MainManager : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = Player.Instance;
        LoadPlayerData();
    }

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
