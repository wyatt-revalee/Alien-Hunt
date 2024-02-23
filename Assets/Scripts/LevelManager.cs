using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Level Settings")]
    public int currentLevel;
    public List<EnemySpawner> enemySpawners;
    public int enemiesInWave;
    public int enemiesKilled;
    public int pointsEarned;
    public int shotsTaken;
    public int shotsHit;
    public int enemiesLeft;
    public bool healthPickupAssigned;

    [Header("Game Objects")]
    public GameObject WaveMessenger;
    public WeaponController weaponController;
    private Weapon weapon;
    private bool waitingOnNewWave;
    public Player player;

    [Header("Pickup Settings")]
    [SerializeField]
    public List<PickupSpawn> pickupInitializers;
    public Transform pickupSpawn;
    public Dictionary<string, GameObject> pickups = new Dictionary<string, GameObject>();

    public event Action<int> OnNewWaveStart;
    public event Action<int> OnEnemyDeath;

    private void Awake()
    {
        // Add listener for new weapons being set
        weaponController.OnNewWeaponSet += NewWeaponSet;

        // Add listener for player health
        player.OnHealthChange += PlayerHealthListener;

        // Add listener for game over
        player.OnGameOver += EndGameHandler;
    }

    void Start()
    {   
        // Convert list to dict for accessing via name easier
        foreach(PickupSpawn pickupSpawn in pickupInitializers)
        {
            pickups.Add(pickupSpawn.name, pickupSpawn.prefab);
        }
        
        WaveMessenger.SetActive(false);

        // Grab all spawners
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

    // Looks for all spawners in scene by tag, adds them to list and adds listeners to their events
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

    // Tells spawners to start new wave
    private void StartSpawners()
    {
        foreach(EnemySpawner spawner in enemySpawners)
        {
            spawner.StartNewWave(currentLevel);
        }
    }

    // Event listener, tracks total enemies in wave
    private void AddEnemyToCount(int enemiesSpawned)
    {
        enemiesLeft += enemiesSpawned;
        enemiesInWave += enemiesSpawned;
        OnNewWaveStart?.Invoke(enemiesInWave);
    }

    //Keeps track of kills and how many enemies are left
    private void RemoveEnemyFromCount(bool killedByPlayer)
    {
        enemiesLeft--;
        if(killedByPlayer)
        {
            enemiesKilled++;
        }
        OnEnemyDeath?.Invoke(enemiesLeft);
    }
    private void AddToPointsEarned(int points)
    {
        pointsEarned += points;
    }

    private void AddToShotsTaken()
    {
        shotsTaken++;
    }

    private void AddToShotsHit()
    {
        shotsHit++;
    }

    private void PlayerHealthListener(int health)
    {
        if(health <= player.maxHealth/3 && !healthPickupAssigned)
        {
            AssignHealthPickup();
        }
        if(health > player.maxHealth/3 && healthPickupAssigned)
        {
            healthPickupAssigned = false;
        }
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
        shotsTaken = 0;
        shotsHit = 0;
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
        waveText.text = string.Format("Wave {0} Complete\nEnemies Killed: {1}/{2}\nPoints Earned: {3}", currentLevel, enemiesKilled, enemiesInWave, pointsEarned);

        float accuracy = GetWaveAccuracy();
        if (accuracy >= 70)
        {
            yield return new WaitForSeconds(1f);
            waveText.text = string.Format("Wave {0} Complete\nEnemies Killed: {1}/{2}\nPoints Earned: {3}\nAccuracy Bonus: {4}", currentLevel, enemiesKilled, enemiesInWave, pointsEarned, (int)accuracy*10);
            weapon.AddBonus((int)accuracy * 10);
        }

    }

    public void AssignHealthPickup()
    {
        int random = UnityEngine.Random.Range(0, enemySpawners.Count);
        enemySpawners[random].dropToAssign = pickups["Health"];
        healthPickupAssigned = true;
    }

    public void AssignMissedPickup(GameObject missedPickup)
    {
        Debug.Log("Assigning missed pickup");
        int random = UnityEngine.Random.Range(0, enemySpawners.Count);
        enemySpawners[random].dropToAssign = missedPickup;
    }

    private void NewWeaponSet()
    {
        weapon = weaponController.currentWeaponScript;
        weapon.OnEnemyKilled += AddToPointsEarned;
        weapon.OnShotFired += AddToShotsTaken;
        weapon.OnEnemyHit += AddToShotsHit;
    }

    // Returns player's accuracy for each wave
    private float GetWaveAccuracy()
    {
        return 100 * ((float)shotsHit / (float)shotsTaken);
    }

    private void EndGameHandler()
    {
        Cursor.visible = true;
        SceneManager.LoadScene(1);
    }

[Serializable]
    public class PickupSpawn
    {
        public string name;
        public GameObject prefab;
    }

}
