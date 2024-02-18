using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;
using UnityEngine.InputSystem;
using static EnemyData;

public class Enemy : MonoBehaviour
{

    public int health;
    public int damage;
    public int speed;
    public EnemyData enemyData;
    public Animator animator;
    public Canvas pointPopup;

    public void Start()
    {
        SetStatsToLevel(1);
    }

    public void SetStatsToLevel(int level)
    {
        foreach (KeyValuePair<string, int> stat in enemyData.UpdateStats())
        {
            switch (stat.Key)
            {
                case "health":
                    health = stat.Value * level;
                    break;

                case "damage":
                    damage = stat.Value * level;
                    break;

                case "speed":
                    speed = stat.Value * level;
                    break;
            }
        }
    }

    public void Damage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            StartCoroutine(Death());
        }
    }

    public IEnumerator Death()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(2f);
        var popup = Instantiate(pointPopup, transform.position, Quaternion.identity);
        popup.GetComponent<PointPopup>().SetValue(enemyData.value);
        Destroy(gameObject);
    }
}
