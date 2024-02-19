using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    public int currentLevel;
    public int typeCount;
    public List<EnemySpawner> enemySpawners;
    public List<int> typesToUse;
    // Start is called before the first frame update
    void Start()
    {
        GetSpawners();
        GenerateTypes();
        StartNewWave();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GetSpawners()
    {
        GameObject[] spawnerObjects = GameObject.FindGameObjectsWithTag("Spawner");
        foreach(GameObject spawner in spawnerObjects)
        {
            enemySpawners.Add(spawner.GetComponent<EnemySpawner>());
        }
    }

    private void GenerateTypes()
    {
        for(int i = 1; i <= typeCount; i++)
        {
            typesToUse.Add(i);
        }
        typesToUse = typesToUse.OrderBy(x => UnityEngine.Random.value).ToList();
    }

    private void StartNewWave()
    {
        for(int i = 0; i < enemySpawners.Count; i++)
        {
            enemySpawners[i].StartNewWave(typesToUse[i], currentLevel);
        }
    }

}
