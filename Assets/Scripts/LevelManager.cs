using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public int currentLevel;
    public int typeCount;
    public List<EnemySpawner> enemySpawners;
    public List<int> typesToUse;
    public int enemiesInWave;
    public int enemiesKilled;
    public int pointsEarned;
    public int enemiesLeft;
    public GameObject WaveMessenger;
    public Crosshair crosshair;
    private bool waitingOnNewWave;

    // Start is called before the first frame update
    void Start()
    {
        crosshair.OnEnemyKilled += AddToPointsEarned;
        WaveMessenger.SetActive(false);
        GetSpawners();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemiesLeft == 0 && !waitingOnNewWave)
        {
            StartCoroutine(StartNewWave());
        }
    }

    private void GetSpawners()
    {
        GameObject[] spawnerObjects = GameObject.FindGameObjectsWithTag("Spawner");
        foreach(GameObject spawner in spawnerObjects)
        {
            EnemySpawner esObj = spawner.GetComponent<EnemySpawner>();
            enemySpawners.Add(esObj);
            esObj.OnEnemySpawned += AddEnemyToCount;
            esObj.OnEnemyDeath += RemoveEnemyFromCount;
        }
    }

    private void GenerateTypes()
    {
        typesToUse.Clear();
        for(int i = 1; i <= typeCount; i++)
        {
            typesToUse.Add(i);
        }
        typesToUse = typesToUse.OrderBy(x => UnityEngine.Random.value).ToList();
    }

    private void StartSpawners()
    {
        for(int i = 0; i < enemySpawners.Count; i++)
        {
            enemySpawners[i].StartNewWave(typesToUse[i], currentLevel);
        }
    }

    private void AddEnemyToCount()
    {
        enemiesLeft++;
        enemiesInWave++;
    }

    private void RemoveEnemyFromCount(bool killedByPlayer)
    {
        enemiesLeft--;
        if(killedByPlayer)
        {
            enemiesKilled++;
        }
    }
    private void AddToPointsEarned(int points)
    {
        pointsEarned += points;
    }


    public IEnumerator StartNewWave()
    {
        if(currentLevel != 0)
        {
            SetEndWaveText();
            WaveMessenger.SetActive(true);
        }
        currentLevel++;
        enemiesKilled = 0;
        enemiesInWave = 0;
        pointsEarned = 0;
        waitingOnNewWave = true;
        yield return new WaitForSeconds(3f);
        WaveMessenger.SetActive(true);
        StartCoroutine(SetNewWaveText(3));
        yield return new WaitForSeconds(4f);
        WaveMessenger.SetActive(false);
        GenerateTypes();
        StartSpawners();
        waitingOnNewWave = false;
    }


    private IEnumerator SetNewWaveText(int secondsToStart)
    {
        TextMeshProUGUI waveText = WaveMessenger.GetComponentInChildren<TextMeshProUGUI>();

        while (secondsToStart > 0)
        {
            waveText.text = string.Format("Wave {0}\nStarting in...\n{1}", currentLevel, secondsToStart);
            yield return new WaitForSeconds(1f); // Wait for 1 second

            secondsToStart--; // Decrease the countdown
        }

        // Countdown finished
        waveText.text = string.Format("Wave {0}\nStarting in...\nNow!", currentLevel);
    }

    private void SetEndWaveText()
    {
        TextMeshProUGUI waveText = WaveMessenger.GetComponentInChildren<TextMeshProUGUI>();
        waveText.text = string.Format("Wave {0}\nEnemies Killed: {1}/{2}\nPoints Eanred: {3}", currentLevel, enemiesKilled, enemiesInWave, pointsEarned);
    }

}
