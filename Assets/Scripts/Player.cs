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
    public int reloadSpeedModifier;


    public int coins;
    public HealthBar healthBar;
    public event Action<int> OnHealthChange;
    public event Action OnGameOver;
    public event Action<int> OnCoinChange;

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

    public void AddUpgrade(string upgrade, float value)
    {
        switch(upgrade)
        {
            case "BulletSize":
                bulletSizeModifer += value;
                break;
            case "FireRate":
                fireRateModifier += value;
                break;
            case "ReloadSpeed":
                reloadSpeedModifier++;
                break;
        }
    }
}
