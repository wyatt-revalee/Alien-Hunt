using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class EnemySpawner : MonoBehaviour
{
    public int spawnerType;
    public int horizontalDirection;
    public int verticalDirection;
    public int currency;
    public float spawnInterval;
    public float spawnTimer;
    public List<GameObject> enemiesToSpawn;
    public List<GameObject> enemyTypes;
    public GameObject enemySpawnType;
    // Start is called before the first frame update

    public event Action OnEnemySpawned;
    public event Action<bool> OnEnemyDeath;

    void FixedUpdate()
    {
        if (spawnTimer <= 0)
        {
            //spawn an enemy
            if (enemiesToSpawn.Count > 0)
            {
                GameObject enemy = (GameObject)Instantiate(enemiesToSpawn[0], transform.position, Quaternion.identity); // spawn first enemy in our list
                enemy.GetComponent<Enemy>().SetMoveDirection(horizontalDirection, verticalDirection);
                enemy.GetComponent<Enemy>().OnDeath += NotifyEnemyDeath;
                enemiesToSpawn.RemoveAt(0); // and remove it
                spawnTimer = spawnInterval;
            }
        }
        else
        {
            spawnTimer -= Time.fixedDeltaTime;
        }
    }

    // Until we run out of currency, generate our chosen type of enemy
    // Subtract cost from currency
    private void GenerateEnemies()
    {
        while (currency > 0)
        {
            if (enemySpawnType.GetComponent<Enemy>().cost <= currency)
            {
                currency -= enemySpawnType.GetComponent<Enemy>().cost;
                enemiesToSpawn.Add(enemySpawnType);
                OnEnemySpawned?.Invoke();
            }
            else
            {
                currency = 0;
            }
        }
    }

    // Iterates through list of our enemy types, finds one that matches the type of our spawner and sets it as our enemy type to be spawned
    private void GetEnemySpawnType()
    {
        enemySpawnType = enemyTypes[spawnerType-1];
    }

    public void StartNewWave(int newType, int waveNum)
    {
        spawnerType = newType;
        currency = 10 * waveNum;
        GetEnemySpawnType();
        spawnInterval = enemySpawnType.GetComponent<Enemy>().cost * 2;
        spawnTimer = spawnInterval;
        GenerateEnemies();
    }

    private void NotifyEnemyDeath(bool playerKilled)
    {
        OnEnemyDeath?.Invoke(playerKilled);
    }

}
