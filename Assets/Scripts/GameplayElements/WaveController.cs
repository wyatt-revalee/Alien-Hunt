using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveController : MonoBehaviour
{

    public int currentWave = 1;
    public int bossWave = 5;
    public int spawnDelay = 1;
    public GameObject[] enemySpawners;
    public List<GameObject> availableEnemies;
    public List<GameObject> enemiesToSpawn;

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
            GameObject randomEnemy = availableEnemies[Random.Range(0, availableEnemies.Count)];
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
        GenerateEnemies();
        foreach (GameObject spawner in enemySpawners)
        {
            spawner.GetComponent<EnemySpawner>().StartWave(spawnDelay);
        }
    }

    public void EndWave()
    {
        
    }

    public void OpenShop()
    {

    }

    public void CloseShop()
    {

    }

}
