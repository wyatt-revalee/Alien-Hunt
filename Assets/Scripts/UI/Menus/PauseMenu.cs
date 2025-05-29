using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    bool isPaused;
    public GameObject controlsMenu;
    public GameObject settingsMenu;
    private GameObject otherMenu; // Reference to another menu that's open (i.e. controls or settings)

    public void PauseHit()
    {
        isPaused = !isPaused;
        Debug.Log("paused");
        gameObject.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1;

    }

    public void Continue()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void Controls()
    {
        controlsMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void Settings()
    {
        Debug.Log("Opening Settings Menu");
        settingsMenu.SetActive(true);
        gameObject.SetActive(false);
    }
    
    public void Exit()
    {
        Debug.Log("Exiting Game");
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1; // Ensure time scale is reset when exiting
    }

}
