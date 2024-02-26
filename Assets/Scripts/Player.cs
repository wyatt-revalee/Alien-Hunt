using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    public int maxHealth;
    public int health;
    public float bulletSizeModifer;
    public float fireRateModifier;
    public float reloadSpeedModifier;
    public float magazineSizeModifier;
    public float damageModifierPercentage;
    public int damageModiferFlat;
    public Dictionary<ItemData, int> inventory = new Dictionary<ItemData, int>();
    public Dictionary<string, float> stats = new Dictionary<string, float>
    {
        { "BulletSize", 1 },
        { "FireRate", 1 },
        { "ReloadSpeed", 1 },
        { "MagazineSize", 1 },
        { "DamagePercent", 1 },
        { "DamageFlat", 1 },
        { "Health", 20 }
    };


    public int coins;
    public HealthBar healthBar;
    public event Action<int> OnHealthChange;
    public event Action OnMagazineSizeChange;
    public event Action OnGameOver;
    public event Action<int> OnCoinChange;
    public event Action OnUpgradeAdded;

    private void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        AddCoins(0);
    }

    public void Heal(int heal)
    {
        health += heal;
        if(health > maxHealth)
        {
            health = maxHealth;
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
        stats[upgrade.upgradeType] += upgrade.upgradeValue;
        OnUpgradeAdded?.Invoke();

        switch(upgrade.upgradeType)
        {
            case "BulletSize":
                bulletSizeModifer += upgrade.upgradeValue;
                break;
            case "FireRate":
                fireRateModifier += upgrade.upgradeValue;
                break;
            case "ReloadSpeed":
                reloadSpeedModifier -= upgrade.upgradeValue;
                break;
            case "MagazineSize":
                magazineSizeModifier += upgrade.upgradeValue;
                OnMagazineSizeChange?.Invoke();
                break;
            case "DamageFlat":
                damageModiferFlat += (int)upgrade.upgradeValue;
                break;
            case "DamagePercent":
                damageModifierPercentage += upgrade.upgradeValue;
                break;
            case "Health":
                maxHealth += (int)upgrade.upgradeValue;
                health = maxHealth;
                healthBar.SetMaxHealth(maxHealth);
                healthBar.SetHealth(health);
                OnHealthChange?.Invoke(health);
                break;
        }
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
                    bulletSizeModifer -= upgrade.upgradeValue;
                    break;
                case "FireRate":
                    fireRateModifier -= upgrade.upgradeValue;
                    break;
                case "ReloadSpeed":
                    reloadSpeedModifier += upgrade.upgradeValue;
                    break;
                case "MagazineSize":
                    magazineSizeModifier -= upgrade.upgradeValue;
                    OnMagazineSizeChange?.Invoke();
                    break;
                case "DamageFlat":
                    damageModiferFlat -= (int)upgrade.upgradeValue;
                    break;
                case "DamagePercentage":
                    damageModifierPercentage -= upgrade.upgradeValue;
                    break;
                case "Health":
                    maxHealth -= (int)upgrade.upgradeValue;
                    health = maxHealth;
                    healthBar.SetMaxHealth(maxHealth);
                    healthBar.SetHealth(health);
                    OnHealthChange?.Invoke(health);
                    break;
            }
        }
    }
}
