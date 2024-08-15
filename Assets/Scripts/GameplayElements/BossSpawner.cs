using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class BossSpawner : MonoBehaviour
{

    public int direction;
    public GameObject currentBoss;
    public List<GameObject> bosses = new List<GameObject>();
    public Action OnBossDeath;

    public void SpawnBoss(int wave)
    {
        currentBoss = Instantiate(bosses[0], transform);
        currentBoss.GetComponent<Enemy>().StartMovement();
        currentBoss.GetComponent<Enemy>().onDeath += OnBossDeath;
    }

    
}
