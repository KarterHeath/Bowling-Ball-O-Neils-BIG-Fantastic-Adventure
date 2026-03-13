using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

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
            Player.Instance.coins = coinManager.coinCount; // Update player's coins before saving
            SaveSystem.SavePlayer(Player.Instance); // Save player data before loading next level
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
        else
        {
            Debug.Log("No more levels!");
        }
    }
    // Detect when the player enters or exits the trigger area
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Door"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Door"))
        {
            playerInRange = false;
        }
    }

}
