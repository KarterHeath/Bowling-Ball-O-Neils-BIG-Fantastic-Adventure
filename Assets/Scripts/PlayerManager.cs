using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Player player;

    private int currentPoints;

    private void Start()
    {
        player = Player.Instance;
    }

    public void SavePlayerData()
    {
        SaveSystem.SavePlayer(player);
    }

    // Example method to add points during gameplay
    public void AddPoints(int amount)
    {
        currentPoints += amount;
    }
}
