using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{

    public int currentWave;
    public GameObject[] enemySpawners;

    void Start()
    {
        GetEnemySpawners();
    }

    private void GetEnemySpawners()
    {
        enemySpawners = GameObject.FindGameObjectsWithTag("EnemySpawner");
        foreach(GameObject spawner in enemySpawners)
        {
            Debug.Log(spawner.name);
        }
    }

    public void StartNewWave()
    {

    }

    public void EndWave()
    {
        
    }

}
