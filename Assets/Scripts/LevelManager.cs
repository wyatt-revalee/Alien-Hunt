using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public int currentLevel;
    public List<EnemySpawner> enemySpawners;
    public int enemiesInWave;
    public int enemiesKilled;
    public int pointsEarned;
    public int enemiesLeft;
    public GameObject WaveMessenger;
    public WeaponController weaponController;
    private Weapon weapon;
    private bool waitingOnNewWave;

    // Start is called before the first frame update
    void Start()
    {
        weaponController.OnNewWeaponSet += NewWeaponSet;
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

    private void StartSpawners()
    {
        foreach(EnemySpawner spawner in enemySpawners)
        {
            spawner.StartNewWave(currentLevel);
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
        waitingOnNewWave = true;
        if(currentLevel != 0)
        {
            StartCoroutine(SetEndWaveText());
            WaveMessenger.SetActive(true);
        }
        yield return new WaitForSeconds(5f);
        enemiesKilled = 0;
        enemiesInWave = 0;
        pointsEarned = 0;
        WaveMessenger.SetActive(true);
        StartCoroutine(SetNewWaveText(3));
        yield return new WaitForSeconds(4f);
        WaveMessenger.SetActive(false);
        StartSpawners();
        waitingOnNewWave = false;
    }


    private IEnumerator SetNewWaveText(int secondsToStart)
    {
        currentLevel++;
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

    private IEnumerator SetEndWaveText()
    {
        TextMeshProUGUI waveText = WaveMessenger.GetComponentInChildren<TextMeshProUGUI>();
        waveText.text = string.Format("Wave {0} Complete", currentLevel);
        yield return new WaitForSeconds(1f);
        waveText.text = string.Format("Wave {0} Complete\nEnemies Killed: {1}/{2}", currentLevel, enemiesKilled, enemiesInWave);
        yield return new WaitForSeconds(1f);
        waveText.text = string.Format("Wave {0} Complete\nEnemies Killed: {1}/{2}\nPoints Eanred: {3}", currentLevel, enemiesKilled, enemiesInWave, pointsEarned);

    }

    private void NewWeaponSet()
    {
        weapon = weaponController.currentWeaponScript;
        weapon.OnEnemyKilled += AddToPointsEarned;
    }

}
