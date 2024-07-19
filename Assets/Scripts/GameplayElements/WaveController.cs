using System.Collections.Generic;
using UnityEngine;
using System;
using System.CodeDom.Compiler;
public class WaveController : MonoBehaviour
{

    public int currentWave = 0;
    public int bossWave = 5;
    public int spawnDelay = 1;
    private int currentEnemyCount;
    private int currentEnemiesKilled;
    public GameObject[] enemySpawners;
    public List<GameObject> availableEnemies;
    public List<GameObject> enemiesToSpawn;
    public bool isShopping = false;

    public event Action<int> OnWaveStarted;
    public event Action OnwaveEnded;

    void Start()
    {
        GetEnemySpawners();
        StartNewWave();
    }

    // Generates enemies randomly from a list, puts them into a list, then distributes that to all of our spawners
    private void GenerateEnemies()
    {
        enemiesToSpawn.Clear();
        int waveCurrency = currentWave * 5;
        while (waveCurrency != 0)
        {
            GameObject randomEnemy = availableEnemies[UnityEngine.Random.Range(0, availableEnemies.Count)];
            if (randomEnemy.GetComponent<Enemy>().cost <= waveCurrency)
            {
                enemiesToSpawn.Add(randomEnemy);
                waveCurrency -= randomEnemy.GetComponent<Enemy>().cost;
                randomEnemy.GetComponent<Enemy>().index = availableEnemies.IndexOf(randomEnemy);
            }
        }
        foreach(GameObject enemySpawner in enemySpawners)
        {
            enemySpawner.GetComponent<EnemySpawner>().FillSpawner(enemiesToSpawn);
        }
        currentEnemyCount = enemiesToSpawn.Count * enemySpawners.Length;
    }

    private void GetEnemySpawners()
    {
        enemySpawners = GameObject.FindGameObjectsWithTag("EnemySpawner");
        foreach(GameObject spawner in enemySpawners)
        {
            spawner.GetComponent<EnemySpawner>().waveController = this;
        }
    }

    public void StartNewWave()
    {
        currentWave++;
        OnWaveStarted?.Invoke(currentWave);
        GenerateEnemies();
        foreach (GameObject spawner in enemySpawners)
        {
            spawner.GetComponent<EnemySpawner>().StartWave(spawnDelay);
        }
    }

    public void EnemyDied()
    {
        currentEnemiesKilled++;

        if(currentEnemiesKilled == currentEnemyCount)
        {
            EndWave();
        }
    }

    public void EndWave()
    {
        OnwaveEnded?.Invoke();
        foreach (GameObject spawner in enemySpawners)
        {
            spawner.GetComponent<EnemySpawner>().EndWave();
        }
        currentEnemiesKilled = 0;
        currentEnemyCount = 0;
        //Debug.Log("All enemies Killed!");
        isShopping = true;
        OpenShop();
    }

    public void OpenShop()
    {
        //Debug.Log("Shopping Time!");
        CloseShop();
    }

    public void CloseShop()
    {
        //Debug.Log("Closing shop");
        isShopping = false;
        StartNewWave();
    }

}
