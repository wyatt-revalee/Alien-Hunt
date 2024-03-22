using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{

    public List<GameObject> bosses;
    public GameObject currentBoss;
    private Dictionary<int, List<GameObject>> bossDictionary = new Dictionary<int, List<GameObject>>();

    private void Start()
    {
        InitializeBossDictionary();
    }

    public void InitializeBossDictionary()
    {
        foreach (GameObject boss in bosses)
        {
            int wave = boss.GetComponent<Boss>().wave;
            if(!bossDictionary.ContainsKey(wave))
            {
                bossDictionary.Add(wave, new List<GameObject>());
            }
            bossDictionary[wave].Add(boss);
        }
    }

    public void SpawnBoss(int currentWave)
    {
        currentBoss = Instantiate(bossDictionary[currentWave][Random.Range(0, bossDictionary[currentWave].Count)], transform.position, transform.rotation);
    }

}