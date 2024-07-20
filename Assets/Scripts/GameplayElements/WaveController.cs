using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
public class WaveController : MonoBehaviour
{

    private int enemyCurrencyMultipler = 1;
    public int waveCountdown = 3;
    public int currentWave = 0;
    public int bossWave = 5;
    public int spawnDelay = 1;
    private int currentEnemyCount;
    private int currentEnemiesKilled;
    public GameObject[] enemySpawners;
    public List<GameObject> availableEnemies;
    public List<GameObject> enemiesToSpawn;
    public bool isShopping = false;
    public Shop shop;
    public WaveUI waveUI;
    public PlayerStats playerStats;

    public event Action<int> OnWaveStarted;
    public event Action<int> OnwaveEnded;

    void Start()
    {
        shop.onShopEnd += CloseShop;
        shop.gameObject.SetActive(false);
        GetEnemySpawners();
        StartNewWave();
    }

    // Generates enemies randomly from a list, puts them into a list, then distributes that to all of our spawners
    private void GenerateEnemies()
    {
        enemiesToSpawn.Clear();
        int waveCurrency = currentWave * enemyCurrencyMultipler;
        while (waveCurrency != 0)
        {
            GameObject randomEnemy = availableEnemies[UnityEngine.Random.Range(0, availableEnemies.Count)];
            if (randomEnemy.GetComponent<Enemy>().cost <= waveCurrency)
            {
                enemiesToSpawn.Add(randomEnemy);
                waveCurrency -= randomEnemy.GetComponent<Enemy>().cost;
                randomEnemy.GetComponent<Enemy>().index = availableEnemies.IndexOf(randomEnemy);
            }
        }
        foreach(GameObject enemySpawner in enemySpawners)
        {
            enemySpawner.GetComponent<EnemySpawner>().FillSpawner(enemiesToSpawn);
        }
        currentEnemyCount = enemiesToSpawn.Count * enemySpawners.Length;
    }

    private void GetEnemySpawners()
    {
        enemySpawners = GameObject.FindGameObjectsWithTag("EnemySpawner");
        foreach(GameObject spawner in enemySpawners)
        {
            spawner.GetComponent<EnemySpawner>().waveController = this;
            spawner.GetComponent<EnemySpawner>().SetEnemyTypes(availableEnemies);
        }
    }

    public void StartNewWave()
    {
        StartCoroutine(NewWaveSequence());
    }

    public IEnumerator NewWaveSequence()
    {
        waveUI.StartWaveCountdown(waveCountdown);
        yield return new WaitForSeconds(waveCountdown);
        currentWave++;
        yield return new WaitForSeconds(1f);
        OnWaveStarted?.Invoke(currentWave);
        GenerateEnemies();
        foreach (GameObject spawner in enemySpawners)
        {
            spawner.GetComponent<EnemySpawner>().StartWave(spawnDelay);
        }
    }

    public void EnemyDied()
    {
        currentEnemiesKilled++;

        if(currentEnemiesKilled == currentEnemyCount)
        {
            EndWave();
        }
    }

    public void EndWave()
    {
        foreach (GameObject spawner in enemySpawners)
        {
            spawner.GetComponent<EnemySpawner>().EndWave();
        }
        StartCoroutine(EndWaveSequence());
        
    }

    public IEnumerator EndWaveSequence()
    {
        playerStats.PrintAllStats();
        OnwaveEnded?.Invoke(currentWave);
        yield return new WaitForSeconds(1f);
        string waveInfo = string.Format("Points Earned: {0}", playerStats.stats["Points"].current);
        waveUI.SetWaveInfoText(waveInfo);
        yield return new WaitForSeconds(1f);
        waveInfo += string.Format("\nEnemies Killed: {0}", playerStats.stats["Enemies Killed"].current);
        waveUI.SetWaveInfoText(waveInfo);
        yield return new WaitForSeconds(3f);
        waveUI.HidePanel();

        currentEnemiesKilled = 0;
        currentEnemyCount = 0;
        //Debug.Log("All enemies Killed!");
        isShopping = true;
        OpenShop();
    }

    public void OpenShop()
    {
        shop.gameObject.SetActive(true);
    }

    public void CloseShop()
    {
        //Debug.Log("Closing shop");
        isShopping = false;
        StartNewWave();
    }


}
