using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killzone : MonoBehaviour
{
    public Player player;
    public LevelManager levelManager;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // If object is an enemy, destroy it
        if (collider.transform.gameObject.layer == 6)
        {
            Enemy enemy = collider.transform.gameObject.GetComponent<Enemy>();
            enemy.HitKillZone();
            if(enemy.itemDrop != null)
            {
                levelManager.AssignMissedPickup(enemy.itemDrop);
            }
        }

        if (collider.transform.gameObject.layer == 9)
        {
            levelManager.AssignMissedPickup(collider.transform.gameObject);
        }
    }
}
