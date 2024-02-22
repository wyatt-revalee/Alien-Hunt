using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.gameObject.layer == 8)
        {
            // Grab the parent weapon of the bullet, and then the weapon controller of that weapon
            Player player = collider.transform.gameObject.GetComponent<Bullet>().weapon.transform.parent.GetComponent<Player>();
            player.Heal(player.maxHealth / 2);
            Destroy(gameObject);
        }
    }
}
