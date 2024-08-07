using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;


public class PlayerStats : MonoBehaviour
{

    public Player player;
    public Dictionary<string, Stat> stats;
    public Action<string> OnStatsUpdated;

    void Awake()
    {
        player = GetComponent<Player>();
        player.OnShotHit += ShotFired;
        player.OnEnemyKilled += EnemyKilled;
        stats = new Dictionary<string, Stat>
        {
            {"Shots Fired", new Stat()},
            {"Shots Hit", new Stat()},
            {"Shots Missed", new Stat()},
            {"Enemies Killed", new Stat()},
            {"Points", new Stat()},
        };
    }

    public void AddPoints(int amount)
    {
        UpdateStat("Points", amount);
    }

    public void ShotFired(bool hitTarget)
    {
        UpdateStat("Shots Fired", 1);
        if (hitTarget)
        {
            UpdateStat("Shots Hit", 1);
        }
        else
        {
            UpdateStat("Shots Missed", 1);
        }
    }

    public void EnemyKilled(int pointsEarned)
    {
        UpdateStat("Enemies Killed", 1);
        UpdateStat("Points", pointsEarned);
    }

    public void UpdateStat(string statToUpdate, int amount)
    {
        stats[statToUpdate].current += amount;
        stats[statToUpdate].total += amount;
        OnStatsUpdated?.Invoke(statToUpdate);
    }

    public void ResetCurrentStats()
    {
        foreach(KeyValuePair<string, Stat> kvp in stats)
        {
            kvp.Value.current = 0;
        }
    }

    public void PrintAllStats()
    {
        foreach (KeyValuePair<string, Stat> kvp in stats)
        {
            Debug.Log(string.Format("{0}: (Current: {1}, Total: {2})", kvp.Key, kvp.Value.current, kvp.Value.total));
        }
    }

}


public class Stat
{
    public int current = 0;
    public int total = 0;
}