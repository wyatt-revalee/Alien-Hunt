using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsMenu : MonoBehaviour
{

    public GameObject pauseMenu;
    public void Exit()
    {
        Debug.Log("Exiting Controls Menu");
        gameObject.SetActive(false);
        pauseMenu.SetActive(true);
    }
}
