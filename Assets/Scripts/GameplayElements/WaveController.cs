using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
public class WaveController : MonoBehaviour
{

    public int enemyCurrencyMultipler; // default 5?
    public int waveCountdown = 3;
    public int currentWave = 0;
    public int bossWave = 5;
    public int spawnDelay = 1;
    private int currentEnemyCount;
    private int currentEnemiesKilled;
    public GameObject bossSpawner;
    public GameObject[] enemySpawners;
    public List<GameObject> availableEnemies;
    public List<GameObject> enemiesToSpawn;
    public bool isShopping = false;
    public Shop shop;
    public WaveUI waveUI;
    public PlayerStats playerStats;
    public Player player;

    public event Action<int> OnWaveStarted;
    public event Action<int> OnWaveEnded;

    void Start()
    {
        shop.onShopEnd += CloseShop;
        shop.gameObject.SetActive(false);
        bossSpawner.GetComponent<BossSpawner>().OnBossDeath += BossDied;
        GetEnemySpawners();
        StartNewWave();
    }

    // Generates enemies randomly from a list, puts them into a list, then distributes that to the spawner
    private void GenerateEnemies()
    {
        // Clears list of enemies to spawn, sets currency, and generates enemies randomly from list of available ones. Adds to list of enemies to be spawned.
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
        
        // Iterates through each spawner and adds the entire list of enemies to be spawned
        // Each spawner will spawn the same enemies in the same order, for a total enemy count of list.length * enemySpawners.count
        foreach(GameObject enemySpawner in enemySpawners)
        {
            enemySpawner.GetComponent<EnemySpawner>().FillSpawner(enemiesToSpawn);
        }
        // Each spawner gets the same list of enemies and spawns their own copy so total enemies = list length times spawner count
        currentEnemyCount = enemiesToSpawn.Count * enemySpawners.Length;
    }

    // Finds spawners by their tag, sets itself as their controller and sets their enemy types
    private void GetEnemySpawners()
    {
        enemySpawners = GameObject.FindGameObjectsWithTag("EnemySpawner");
        foreach(GameObject spawner in enemySpawners)
        {
            spawner.GetComponent<EnemySpawner>().waveController = this;
            spawner.GetComponent<EnemySpawner>().SetEnemyTypes(availableEnemies);
        }
    }


    // Starts new wave
    public void StartNewWave()
    {

        // If current wave is -1, start wave but spawn nothing. Endless wave for testing
        if(currentWave == -1)
        {
            Debug.Log("Starting testing wave");
            StartCoroutine(NewWaveSequence(false));
        }

        // Check if it should be a boss wave, start wave sequence and pass that info along
        else
        {
            Debug.Log(string.Format("Current wave: {0}, Boss Wave: {1}, Value: {2}", currentWave + 1, bossWave, (currentWave+1)%bossWave));
            bool isBossWave = (currentWave + 1) % bossWave == 0 ? true : false; // Check if next wave lines up as a boss wave. Ex: boss wave = 5, so waves would be 5, 10, 15...25...50. etc.
            StartCoroutine(NewWaveSequence(isBossWave));
        }

    }


    // Tells UI to start countdown, runs enemy generator, and starts spawnesr
    public IEnumerator NewWaveSequence(bool isBossWave)
    {
        waveUI.StartWaveCountdown(waveCountdown);
        yield return new WaitForSeconds(waveCountdown);
        currentWave++;
        yield return new WaitForSeconds(1f);
        OnWaveStarted?.Invoke(currentWave);

        // If wave is a boss wave, spawn boss, else spawn regular enemies
        if(isBossWave)
        {
            bossSpawner.GetComponent<BossSpawner>().SpawnBoss(currentWave);
        }
        else
        {
            GenerateEnemies();
            foreach (GameObject spawner in enemySpawners)
            {
                spawner.GetComponent<EnemySpawner>().StartWave(spawnDelay);
            }
        }
    }


    // Called upon enemy deaths. Keeps track of total killed and checks if wave should be ended.
    public void EnemyDied()
    {
        currentEnemiesKilled++;

        if(currentEnemiesKilled == currentEnemyCount)
        {
            EndWave();
        }
    }


    // Called upon boss death. Runs routine
    public void BossDied()
    {
        //do boss death stuff
        StartCoroutine(EndWaveSequence());
    }


    // Tells spawners that the wave is over, starts end of wave sequence
    public void EndWave()
    {
        foreach (GameObject spawner in enemySpawners)
        {
            spawner.GetComponent<EnemySpawner>().EndWave();
        }
        StartCoroutine(EndWaveSequence());
    }

    // Ran to award player coins upon end of wave
    private void AwardPlayerCoins()
    {
        int amountToAward = 0;
        player.AddCoins(amountToAward);
    }

    // Signals end of wave action to listeners and tells end of wave UI to show stats, then opens shop
    public IEnumerator EndWaveSequence()
    {
        //playerStats.PrintAllStats();
        OnWaveEnded?.Invoke(currentWave);

        yield return new WaitForSeconds(1f);
        string waveInfo = string.Format("Points Earned: {0}", playerStats.stats["Points"].current);
        waveUI.SetWaveInfoText(waveInfo);

        yield return new WaitForSeconds(1f);
        waveInfo += string.Format("\nEnemies Killed: {0}", playerStats.stats["Enemies Killed"].current);
        waveUI.SetWaveInfoText(waveInfo);

        int accuracy = (int)(100 * (((float)playerStats.stats["Shots Hit"].current) / ((float)playerStats.stats["Shots Fired"].current)));
        if(accuracy > 50)
        {
            yield return new WaitForSeconds(1f);
            waveInfo += string.Format("\nAccuracy Bonus: + {0} points", accuracy * currentWave);
            playerStats.AddPoints(accuracy * currentWave);
            waveUI.SetWaveInfoText(waveInfo);
        }

        yield return new WaitForSeconds(1f);
        waveInfo += string.Format("\nMoney Earned: {0}", playerStats.stats["Points"].current/10);
        waveUI.SetWaveInfoText(waveInfo);
        player.AddCoins(playerStats.stats["Points"].current / 10);

        yield return new WaitForSeconds(3f);
        waveUI.HidePanel();
        playerStats.stats["Enemies Killed"].current = 0;
        playerStats.stats["Points"].current = 0;
        currentEnemiesKilled = 0;
        currentEnemyCount = 0;


        //Debug.Log("All enemies Killed!");
        isShopping = true;
        OpenShop();
    }


    // Sets shop to active and refreshes it
    public void OpenShop()
    {
        shop.gameObject.SetActive(true);
        shop.RefreshShop();
    }

    // Called upon shop closing. Starts new wave.
    public void CloseShop()
    {
        //Debug.Log("Closing shop");
        isShopping = false;
        StartNewWave();
    }


}
