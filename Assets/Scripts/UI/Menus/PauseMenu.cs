using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    
    bool isPaused;

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

}
