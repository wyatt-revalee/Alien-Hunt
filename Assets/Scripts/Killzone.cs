using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killzone : MonoBehaviour
{
    public Player player;
    public LevelManager levelManager;

    // Destory enemies that leave the screen, send their currency back to spawner and have it spawn a new enemy
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.gameObject.layer == 6)
        {
            Enemy enemy = collider.transform.gameObject.GetComponent<Enemy>();
            if (enemy.itemDrop != null)
            {
                levelManager.AssignMissedPickup(enemy.itemDrop);
            }
            Debug.Log(enemy.gameObject.name + " has left the screen");
            enemy.parentSpawner.GetComponent<EnemySpawner>().RespawnEnemy(enemy.gameObject.name.Substring(0, enemy.gameObject.name.Length - 7));

            Destroy(collider.transform.gameObject);
        }
    }
}
