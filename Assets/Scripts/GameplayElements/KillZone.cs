using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KillZone : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // If object is an enemy, destroy it and send back to spawner to be respawned
        if(collider.gameObject.layer == 9)
        {
            collider.GetComponent<Enemy>().parentSpawner.GetComponent<EnemySpawner>().RespawnEnemy(collider.GetComponent<Enemy>().index);
            Destroy(collider.gameObject);
        }

        if (collider.gameObject.layer == 12)
        {
            Destroy(collider.gameObject);
        }
    }

}