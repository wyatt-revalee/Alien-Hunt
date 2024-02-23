using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject settingsMenu;
    public WeaponController weaponController;

    public void Resume()
    {
        weaponController.PauseGame();
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
