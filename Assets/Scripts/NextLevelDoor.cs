using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class NextLevelDoor : MonoBehaviour
{
    public CoinManager coinManager; // Reference to CoinManager to access player's coins
    private bool playerInRange = false;

    // This function gets called when Interact is pressed
    public void OnInteract(InputValue context)
    {
        // Only trigger when button is pressed (not released)
        if (context.isPressed && playerInRange)
        {
            Player.Instance.coins = coinManager.coinCount;// Update player's coins before saving
            SaveSystem.SavePlayer(Player.Instance);
            LoadNextLevel();
        }
    }
    // Load the next level based on the current scene index
    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }

    }
    // Detect when the player enters or exits the trigger area
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Door"))
        {
            UnlockNewLevel(); // Unlock the next level when the player enters the door trigger
            playerInRange = true;
            SaveSystem.SavePlayer(Player.Instance); // Save player data after unlocking new level
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Door"))
        {
            playerInRange = false;
        }
    }
    void UnlockNewLevel()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Level1":
                if (Player.Instance.level < 2)
                    Player.Instance.level = 2;
                // Unlock Level 2
                break;
            case "Level2":
                if (Player.Instance.level < 3)
                    Player.Instance.level = 3;
                // Unlock Level 3
                break;
            case "Level3":
                if (Player.Instance.level < 4)
                    Player.Instance.level = 4;
                // Unlock Level 4
                break;
            case "Level4":
                if (Player.Instance.level < 5)
                    Player.Instance.level = 5;
                // Unlock Level 
                break;
                // Add more cases for additional levels
        }
    }
}

