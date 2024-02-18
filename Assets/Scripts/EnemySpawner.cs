using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int spawnerType;
    public int horizontalDirection;
    public int verticalDirection;
    public int currency;
    public float spawnInterval;
    public float spawnTimer;
    public List<GameObject> enemiesToSpawn;
    public List<EnemySpawn> enemies;
    public EnemySpawn enemySpawnType;
    // Start is called before the first frame update
    void Start()
    {
        StartNewWave(spawnerType, 1);
    }

    void FixedUpdate()
    {
        if (spawnTimer <= 0)
        {
            //spawn an enemy
            if (enemiesToSpawn.Count > 0)
            {
                GameObject enemy = (GameObject)Instantiate(enemiesToSpawn[0], transform.position, Quaternion.identity); // spawn first enemy in our list
                enemy.GetComponent<Enemy>().SetMoveDirection(horizontalDirection, verticalDirection);
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
            if (enemySpawnType.cost <= currency)
            {
                currency -= enemySpawnType.cost;
                enemiesToSpawn.Add(enemySpawnType.enemyPrefab);
            }
        }
    }

    // Iterates through list of our enemy types, finds one that matches the type of our spawner and sets it as our enemy type to be spawned
    private void GetEnemySpawnType()
    {
        foreach(EnemySpawn e in enemies)
        {
            if(e.type == spawnerType)
            {
                enemySpawnType = e;
            }
        }
    }

    public void StartNewWave(int newType, int waveNum)
    {
        spawnerType = newType;
        currency = 10 * waveNum;
        GetEnemySpawnType();
        GenerateEnemies();
    }



    [System.Serializable]
    public class EnemySpawn
    {
        public GameObject enemyPrefab;
        public int cost;
        public int type;
    }

}
