using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public List<GameObject> availableEnemies;
    public List<GameObject> enemiesToSpawn;

    public void FillSpawner(List<GameObject> enemies)
    {
        enemiesToSpawn = enemies;
    }

    public void StartWave()
    {
        foreach(GameObject enemy in enemiesToSpawn)
        {
            Debug.Log(enemy.GetComponent<Enemy>().cost);
        }
    }

}
