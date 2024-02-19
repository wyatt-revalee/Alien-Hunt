using System;
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
    public Weapon weapon;

    private void Start()
    {
        weapon = weaponController.currentWeaponScript;
        UpdateStatsUI();
        weapon.OnEnemyKilled += EnemyDeathHandler;
        weapon.OnShotFired += ShotFiredHandler;
        weapon.OnEnemyHit += EnemyHitHandler;
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
}
