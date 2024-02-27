using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    public Stat maxHealth;
    public int health;
    public Stat bulletSizeModifer;
    public Stat fireRateModifier;
    public Stat reloadSpeedModifier;
    public Stat magazineSizeModifier;
    public Stat damageModifierPercentage;
    public Stat damageModiferFlat;
    public Dictionary<ItemData, int> inventory = new Dictionary<ItemData, int>();
    public Dictionary<string, Stat> stats = new Dictionary<string, Stat>();
    public int coins;
    public HealthBar healthBar;
    public event Action<int> OnHealthChange;
    public event Action OnMagazineSizeChange;
    public event Action OnGameOver;
    public event Action<int> OnCoinChange;
    public event Action OnUpgradeAdded;

    private void Start()
    {
        health = (int)maxHealth.value;
        healthBar.SetMaxHealth(maxHealth.value);
        AddCoins(0);
        InitializeStats();
    }

    public void Heal(int heal)
    {
        health += heal;
        if(health > maxHealth.value)
        {
            health = (int)maxHealth.value;
        }
        healthBar.SetHealth(health);
        OnHealthChange?.Invoke(health);
    }

    public void Damage(int damage)
    {
        if(health <= 0)
        {
            return;
        }
        health -= damage;
        healthBar.SetHealth(health);
        if(health <= 0)
        {
            StartCoroutine(GameOver());
        }
        StartCoroutine(ShakeScreen(5));
        OnHealthChange?.Invoke(health);
    }

    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1f);
        OnGameOver?.Invoke();
    }

    IEnumerator ShakeScreen(int force)
    {

        // Get initial position
        float xpos = Camera.main.transform.position.x;
        float ypos = Camera.main.transform.position.y;
        float zpos = Camera.main.transform.position.z;

        // Move up and right
        Camera.main.transform.position = new Vector3(xpos + (0.01f * force), ypos + (0.01f * force), zpos);
        yield return new WaitForSeconds(0.01f);

        // Move down and left
        Camera.main.transform.position = new Vector3(xpos - (0.01f * force), ypos - (0.01f * force), zpos);
        yield return new WaitForSeconds(0.01f);

        Camera.main.transform.position = new Vector3(xpos, ypos, zpos);

    }

    public void UpdateHealth()
    {
        health = (int)maxHealth.value;
        healthBar.SetMaxHealth(maxHealth.value);
        healthBar.SetHealth(health);
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        OnCoinChange?.Invoke(coins);
    }

    public void RemoveCoins(int amount)
    {
        coins -= amount;
        OnCoinChange?.Invoke(coins);
    }

    public void AddUpgrade(ItemData upgrade)
    {
        if(!inventory.ContainsKey(upgrade))
        {
            inventory.Add(upgrade, 0);
        }
        inventory[upgrade]++;
        stats[upgrade.upgradeType].value += upgrade.upgradeValue;
        OnUpgradeAdded?.Invoke();
        OnHealthChange?.Invoke(health);
        OnMagazineSizeChange?.Invoke();
    }

    public void RemoveUpgrade(ItemData upgrade)
    {
        if(inventory.ContainsKey(upgrade))
        {
            inventory[upgrade]--;
            if(inventory[upgrade] == 0)
            {
                inventory.Remove(upgrade);
            }
            switch(upgrade.name)
            {
                case "BulletSize":
                    bulletSizeModifer.value -= upgrade.upgradeValue;
                    break;
                case "FireRate":
                    fireRateModifier.value -= upgrade.upgradeValue;
                    break;
                case "ReloadSpeed":
                    reloadSpeedModifier.value += upgrade.upgradeValue;
                    break;
                case "MagazineSize":
                    magazineSizeModifier.value -= upgrade.upgradeValue;
                    OnMagazineSizeChange?.Invoke();
                    break;
                case "DamageFlat":
                    damageModiferFlat.value -= (int)upgrade.upgradeValue;
                    break;
                case "DamagePercent":
                    damageModifierPercentage.value -= upgrade.upgradeValue;
                    break;
                case "Health":
                    maxHealth.value -= (int)upgrade.upgradeValue;
                    health = (int)maxHealth.value;
                    healthBar.SetMaxHealth(maxHealth.value);
                    healthBar.SetHealth(health);
                    OnHealthChange?.Invoke(health);
                    break;
            }
        }
    }

    private void InitializeStats()
    {
        stats.Add("BulletSize", bulletSizeModifer);
        stats.Add("FireRate", fireRateModifier);
        stats.Add("ReloadSpeed", reloadSpeedModifier);
        stats.Add("MagazineSize", magazineSizeModifier);
        stats.Add("DamageFlat", damageModiferFlat);
        stats.Add("DamagePercent", damageModifierPercentage);
        stats.Add("Health", maxHealth);
        OnUpgradeAdded?.Invoke();
    
    }
}

[System.Serializable]
public class Stat
{
    public string name;
    public float value;
    public Sprite sprite;
}