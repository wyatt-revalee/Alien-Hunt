using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    public void Exit()
    {
        Debug.Log("Exiting Settings Menu");
        gameObject.SetActive(false);
        pauseMenu.SetActive(true);
    }

}
