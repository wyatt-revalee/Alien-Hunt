using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public HealthBar healthBar;
    public event Action<int> OnHealthChange;
    public event Action OnGameOver;

    private void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
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
        OnGameOver?.Invoke();
        yield return new WaitForSeconds(5f);
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
}
