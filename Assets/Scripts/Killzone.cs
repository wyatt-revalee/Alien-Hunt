using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killzone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // If object is an enemy, destroy it
        if (collider.transform.gameObject.layer == 6)
        {
            collider.transform.gameObject.GetComponent<Enemy>().HitKillZone();
        }
    }
}
