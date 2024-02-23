using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class StatTracker : MonoBehaviour
{
    [Serializable]
    public class StatisticData
    {
        public int value;
        public string text;
        public TextMeshProUGUI textField;

        public void UpdateUI()
        {
            if (textField != null)
            {
                textField.text = text + '\n' +  value.ToString();
            }
        }
    }

    [Header("Values")]
    public StatisticData totalPoints;
    public StatisticData totalKills;
    public StatisticData totalShots;
    public StatisticData totalHits;

    public TextMeshProUGUI accuracyText;

    public WeaponController weaponController;
    private Weapon weapon;

    private void Awake()
    {
        weaponController.OnNewWeaponSet += NewWeaponSet;
    }
    private void Start()
    {
        UpdateStatsUI();
    }

    private void EnemyDeathHandler(int points)
    {
        totalKills.value++;
        totalPoints.value += points;
        UpdateStatsUI();
    }

    private void ShotFiredHandler()
    {
        totalShots.value++;
        UpdateStatsUI();
    }

    private void EnemyHitHandler()
    {
        totalHits.value++;
        UpdateStatsUI();
    }

    private void BonusAddedHandler(int bonusPoints)
    {
        totalPoints.value += bonusPoints;
        UpdateStatsUI();
    }

    private void UpdateStatsUI()
    {
        totalPoints.UpdateUI();
        totalKills.UpdateUI();
        totalShots.UpdateUI();
        totalHits.UpdateUI();
        UpdateAccuracyUI();
    }

    private void UpdateAccuracyUI()
    {
        float accuracy = totalShots.value > 0 ? (float)totalHits.value / totalShots.value : 0f;
        accuracyText.text = "Accuracy:\n" + accuracy.ToString("P0"); // Display as percentage
    }

    private void NewWeaponSet()
    {
        weapon = weaponController.currentWeaponScript;
        weapon.OnEnemyKilled += EnemyDeathHandler;
        weapon.OnShotFired += ShotFiredHandler;
        weapon.OnEnemyHit += EnemyHitHandler;
        weapon.OnBonusAdded += BonusAddedHandler;
    }

    public IEnumerator InitializeFirstWeapon()
    {
        yield return new WaitForSeconds(3f);
        NewWeaponSet();
    }
}
