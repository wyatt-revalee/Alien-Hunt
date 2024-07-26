using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveEquipment : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.layer == 6)
        {
            Player player = collider2D.GetComponent<Player>();

            if(player.activeEquipment != null)
            {
                player.RemoveEquipment();
            }
            Destroy(gameObject);
        }
    }
}
