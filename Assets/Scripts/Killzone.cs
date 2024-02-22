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
            collider.transform.gameObject.GetComponent<Enemy>().HitKillZone();
            if(collider.transform.gameObject.GetComponent<Enemy>().itemDrop != null)
            {
                levelManager.AssignMissedPickup(collider.transform.gameObject.GetComponent<Enemy>().itemDrop);
            }
            player.Damage(1);
        }

        if (collider.transform.gameObject.layer == 9)
        {
            levelManager.AssignMissedPickup(collider.transform.gameObject);
        }
    }
}
