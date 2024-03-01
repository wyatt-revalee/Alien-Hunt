using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCatcher : MonoBehaviour
{
    // Destroy bullets that leave the screen on collision
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // If object is an enemy, destroy it
        if (collider.transform.gameObject.layer == 8)
        {
            Destroy(collider.transform.gameObject);
        }

    }
}
