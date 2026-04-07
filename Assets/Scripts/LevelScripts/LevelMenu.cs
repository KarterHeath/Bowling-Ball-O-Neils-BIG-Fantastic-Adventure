using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;


public class LevelsMenu : MonoBehaviour
{
     public Button[] buttons;
    private void Awake()
    {
        int unlockedLevel = Player.Instance.level;
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable =false;
        }
        for (int i = 0; i < unlockedLevel; i++)
        {
            buttons[i].interactable = true;
        }
    }
    public void OpenLevel(int levelId)
    {

        string levelName = "Level" + levelId;
        SceneManager.LoadScene(levelName);
    }

}
