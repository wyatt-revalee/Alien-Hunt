using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class EnemySpawner : MonoBehaviour
{
    public int horizontalDirection;
    public int verticalDirection;
    public int currency;
    public float spawnInterval;
    public float spawnTimer;
    private bool generatingEnemies;
    public int currentWave;
    public List<GameObject> enemiesToSpawn;
    public List<GameObject> enemyTypes;
    // Start is called before the first frame update

    public event Action<int> OnEnemySpawned;
    public event Action<bool> OnEnemyDeath;
    public GameObject dropToAssign;

    void FixedUpdate()
    {
        if(generatingEnemies)
        {
            return;
        }
        if (spawnTimer <= 0)
        {
            //spawn an enemy
            if (enemiesToSpawn.Count > 0)
            {
                GameObject enemy = (GameObject)Instantiate(enemiesToSpawn[0], transform.position, Quaternion.identity); // spawn first enemy in our list
                enemy.GetComponent<Enemy>().parentSpawner = gameObject;
                enemy.GetComponent<Enemy>().SetMoveDirection(horizontalDirection, verticalDirection);
                enemy.GetComponent<Enemy>().OnDeath += NotifyEnemyDeath;

                // If spawn has a drop to assign, assign it to the enemy, then nullify it
                if(dropToAssign != null)
                {
                    enemy.GetComponent<Enemy>().itemDrop = dropToAssign;
                    enemy.GetComponent<SpriteRenderer>().color = new Color(200, 200, 200);
                    dropToAssign = null;
                }

                enemiesToSpawn.RemoveAt(0); // and remove it
                spawnTimer = spawnInterval;
            }
        }
        else
        {
            spawnTimer -= Time.fixedDeltaTime;
        }
    }

    // Until we run out of currency, generate random enemy
    // Subtract cost from currency
    public void GenerateEnemies()
    {
        generatingEnemies = true;
        while (currency > 0)
        {
            GameObject randomEnemy = enemyTypes[UnityEngine.Random.Range(0, enemyTypes.Count)];
            if (randomEnemy.GetComponent<Enemy>().cost <= currency && randomEnemy.GetComponent<Enemy>().introWave <= currentWave)
            {
                currency -= randomEnemy.GetComponent<Enemy>().cost;
                enemiesToSpawn.Add(randomEnemy);
            }
        }
        OnEnemySpawned?.Invoke(enemiesToSpawn.Count);
        generatingEnemies = false;
    }

    public void RespawnEnemy(string name)
    {
        GameObject enemy = enemyTypes.Find(x => x.name == name);
        enemiesToSpawn.Add(enemy);
    }

    public void StartNewWave(int waveNum, int newInterval)
    {
        currency = 10 * waveNum;
        currentWave = waveNum;
        GenerateEnemies();
        spawnInterval = newInterval;
        spawnTimer = spawnInterval;
    }

    private void NotifyEnemyDeath(bool playerKilled)
    {
        OnEnemyDeath?.Invoke(playerKilled);
    }

}
