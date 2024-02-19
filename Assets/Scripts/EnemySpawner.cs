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
    public List<GameObject> enemiesToSpawn;
    public List<GameObject> enemyTypes;
    // Start is called before the first frame update

    public event Action OnEnemySpawned;
    public event Action<bool> OnEnemyDeath;

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
                enemy.GetComponent<Enemy>().SetMoveDirection(horizontalDirection, verticalDirection);
                enemy.GetComponent<Enemy>().OnDeath += NotifyEnemyDeath;
                enemiesToSpawn.RemoveAt(0); // and remove it
                Debug.Log(enemiesToSpawn.Count);
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
    private void GenerateEnemies()
    {
        generatingEnemies = true;
        while (currency > 0)
        {
            GameObject randomEnemy = enemyTypes[UnityEngine.Random.Range(0, enemyTypes.Count)];
            if (randomEnemy.GetComponent<Enemy>().cost <= currency)
            {
                currency -= randomEnemy.GetComponent<Enemy>().cost;
                enemiesToSpawn.Add(randomEnemy);
                OnEnemySpawned?.Invoke();
            }
        }
        generatingEnemies = false;
    }

    public void StartNewWave(int waveNum)
    {
        currency = 10 * waveNum;
        GenerateEnemies();
        spawnInterval = UnityEngine.Random.Range(1, Mathf.Max(2, enemyTypes.Count - (waveNum/2)));
        spawnTimer = spawnInterval;
    }

    private void NotifyEnemyDeath(bool playerKilled)
    {
        OnEnemyDeath?.Invoke(playerKilled);
    }

}
