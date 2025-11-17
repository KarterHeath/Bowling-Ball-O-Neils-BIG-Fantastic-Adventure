using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // Quit the game
    public void QuittheGame()
    {
        Debug.Log("Quit Game pressed!"); // This will show in the editor for testing
        Application.Quit(); // This actually quits in a build
    }
}