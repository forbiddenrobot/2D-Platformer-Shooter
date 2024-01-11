using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    public void LoadLevel(int level)
    {
        RemoveActionsTriggered();
        SceneManager.LoadScene("Level " + level);
    }

    public void NextScene()
    {
        RemoveActionsTriggered();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LevelSelector()
    {
        RemoveActionsTriggered();
        SceneManager.LoadScene("Level Selector");
    }

    public void MainMenu()
    {
        RemoveActionsTriggered();
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void NextSong()
    {
        GameObject.Find("BG Music").GetComponent<BackgroundMusic>().NextSong();
    }

    public void PreviousSong()
    {
        GameObject.Find("BG Music").GetComponent<BackgroundMusic>().PreviousSong();
    }

    private void RemoveActionsTriggered()
    {
        PlayerInputHandler[] playerInputHandlers = FindObjectsOfType<PlayerInputHandler>();

        foreach (PlayerInputHandler inputHandler in playerInputHandlers)
        {
            inputHandler.RemoveActionTriggered();
        }
    }
}
