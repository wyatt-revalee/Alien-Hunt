using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int currentLevel;
    public int typeCount;
    public List<EnemySpawner> enemySpawners;
    private List<int> typesToUse;
    // Start is called before the first frame update
    void Start()
    {
        GetSpawners();
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
    }
}
