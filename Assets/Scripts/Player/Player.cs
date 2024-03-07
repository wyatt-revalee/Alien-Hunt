using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    public CharacterStats baseStats;
    public Stat maxHealth;
    public int health;
    public float movementSpeed;
    public Stat movementSpeedModifer;
    public Stat bulletSpeedModifier;
    public Stat bulletSizeModifer;
    public Stat fireRateModifier;
    public Stat reloadSpeedModifier;
    public Stat magazineSizeModifier;
    public Stat damageModifierPercentage;
    public Stat damageModiferFlat;
    public Stat immunityTimeModifier;
    public Dictionary<Item, int> inventory = new Dictionary<Item, int>();
    public Dictionary<string, Stat> stats = new Dictionary<string, Stat>();
    public int coins;
    public HealthBar healthBar;
    public event Action<int> OnHealthChange;
    public event Action OnMagazineSizeChange;
    public event Action OnGameOver;
    public event Action<int> OnCoinChange;
    public event Action OnUpgradeAdded;

    private void Awake()
    {
        InitializeStats();
        health = (int)maxHealth.value;
        healthBar.SetMaxHealth(maxHealth.value);
        AddCoins(0);
    }
    private void Start()
    {
        StartCoroutine(UpdateStats());
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
        StartCoroutine(FlashRoutine());
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

    public IEnumerator FlashRoutine()
    {
        Debug.Log("Flashing");
        GetComponent<BoxCollider2D>().enabled = false;
        // Flash twice, once every 0.5 seconds
        for (int flashCount = 0; flashCount < 2; flashCount++)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(0.25f); 
            GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(0.25f);
        }
        GetComponent<BoxCollider2D>().enabled = true;
    }

    public void SetMaxHealth(int amount)
    {
        maxHealth.value = amount;
        health = (int)maxHealth.value;
        healthBar.SetMaxHealth(maxHealth.value);
        OnHealthChange?.Invoke(health);
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

    public void AddItemToInventory(Item upgrade)
    {
        if(!inventory.ContainsKey(upgrade))
        {
            inventory.Add(upgrade, 0);
        }
        inventory[upgrade]++;
        StartCoroutine(UpdateMagazineSize());
    }

    public void RemoveItemFromInventory(Item upgrade)
    {
        if(inventory.ContainsKey(upgrade))
        {
            inventory[upgrade]--;
            if(inventory[upgrade] == 0)
            {
                inventory.Remove(upgrade);
            }
        }
        StartCoroutine(UpdateMagazineSize());
    }

    // Calls an update to the player's magazine size a second after the player has gotten/lost an item
    public IEnumerator UpdateMagazineSize()
    {
        yield return new WaitForSeconds(1f);
        OnMagazineSizeChange?.Invoke();
    }

    // Updates players stats based on the items in their inventory, every second
    public IEnumerator UpdateStats()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            foreach (var item in inventory)
            {
                item.Key.UpdatePlayer(this, item.Value);
            }
            OnUpgradeAdded?.Invoke();
            OnHealthChange?.Invoke(health);
        }
    }

    private void InitializeStats()
    {
        SetBaseStatValue(maxHealth, baseStats.maxHealth);
        SetBaseStatValue(bulletSpeedModifier, baseStats.bulletSpeedModifier);
        SetBaseStatValue(bulletSizeModifer, baseStats.bulletSizeModifer);
        SetBaseStatValue(fireRateModifier, baseStats.fireRateModifier);
        SetBaseStatValue(reloadSpeedModifier, baseStats.reloadSpeedModifier);
        SetBaseStatValue(magazineSizeModifier, baseStats.magazineSizeModifier);
        SetBaseStatValue(damageModiferFlat, baseStats.damageModiferFlat);
        SetBaseStatValue(damageModifierPercentage, baseStats.damageModifierPercentage);
        SetBaseStatValue(movementSpeedModifer, baseStats.movementSpeed);
        SetBaseStatValue(immunityTimeModifier, baseStats.immunityTimeModifier);

        stats.Add("Speed", movementSpeedModifer);
        stats.Add("BulletSpeed", bulletSpeedModifier);
        stats.Add("BulletSize", bulletSizeModifer);
        stats.Add("FireRate", fireRateModifier);
        stats.Add("ReloadSpeed", reloadSpeedModifier);
        stats.Add("MagazineSize", magazineSizeModifier);
        stats.Add("DamageFlat", damageModiferFlat);
        stats.Add("DamagePercent", damageModifierPercentage);
        stats.Add("MaxHealth", maxHealth);
        stats.Add("ImmunityTime", immunityTimeModifier);
        OnUpgradeAdded?.Invoke();
    }

    private void SetBaseStatValue(Stat playerStat, Stat baseStat)
    {
        playerStat.value = baseStat.value;
        playerStat.name = baseStat.name;
        playerStat.sprite = baseStat.sprite;
    }
}

