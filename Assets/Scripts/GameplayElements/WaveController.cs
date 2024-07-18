using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveController : MonoBehaviour
{

    public int currentWave = 1;
    public int bossWave = 5;
    public GameObject[] enemySpawners;
    public List<GameObject> availableEnemies;
    public List<GameObject> enemiesToSpawn;

    void Start()
    {
        GetEnemySpawners();
        StartNewWave();
    }

    private void GenerateEnemies()
    {
        enemiesToSpawn.Clear();
        int waveCurrency = currentWave * 5;
        Debug.Log(name);
        while (waveCurrency != 0)
        {
            GameObject randomEnemy = availableEnemies[Random.Range(0, availableEnemies.Count)];
            if (randomEnemy.GetComponent<Enemy>().cost <= waveCurrency)
            {
                enemiesToSpawn.Add(randomEnemy);
                waveCurrency -= randomEnemy.GetComponent<Enemy>().cost;
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
    }

    public void StartNewWave()
    {
        GenerateEnemies();
        foreach (GameObject spawner in enemySpawners)
        {
            spawner.GetComponent<EnemySpawner>().StartWave();
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
