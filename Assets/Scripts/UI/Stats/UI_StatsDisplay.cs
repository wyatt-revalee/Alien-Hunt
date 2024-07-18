using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class UI_StatsDisplay : MonoBehaviour
{

    public PlayerStats playerStats;
    public GameObject statBase;
    Dictionary<string, GameObject> displayedStats = new Dictionary<string, GameObject>();

    void Start()
    {
        playerStats.OnStatsUpdated += UpdateStat;
        foreach (KeyValuePair<string, Stat> kvp in playerStats.stats)
        {
            GameObject newStatBase = Instantiate(statBase, transform.position, quaternion.identity);
            newStatBase.GetComponent<UI_Stat>().SetStatInfo(kvp.Key, kvp.Value);
            newStatBase.transform.SetParent(this.transform);
            newStatBase.transform.localScale = new Vector3(0.3f, 0.3f, 1);
            displayedStats.Add(kvp.Key, newStatBase);

        }
    }

    void UpdateStat(string statToUpdate)
    {
        displayedStats[statToUpdate].GetComponent<UI_Stat>().SetStatInfo(statToUpdate, playerStats.stats[statToUpdate]);
    }

}
