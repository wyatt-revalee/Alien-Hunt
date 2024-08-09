using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public PlayerStats playerStats;
    int currentScene;
    private static LevelManager instance;

    void Awake()
    {
        // If there's already a copy of me, self-destruct and abort!
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
        currentScene = SceneManager.GetActiveScene().buildIndex;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene sceneName, LoadSceneMode mode)
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        switch(currentScene)
        {
            case 0:
                break;
            case 1:
                playerStats = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerStats>();
                break;
            case 2:
                GameObject.FindGameObjectsWithTag("Menu")[0].GetComponent<GameOverMenu>().DisplayStats(playerStats.stats);
                break;
            default:
                break;
        }
    }

}
