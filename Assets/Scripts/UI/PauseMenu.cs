using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject settingsMenu;
    public PlayerController playerController;

    public void Resume()
    {
        playerController.PauseGame();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void OpenSettings()
    {
        // Open settings menu
        settingsMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
