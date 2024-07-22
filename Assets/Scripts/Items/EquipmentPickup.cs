using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPickup : MonoBehaviour
{

    public Item equipmentItem;

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.layer == 6)
        {
            Player player = collider2D.GetComponent<Player>();

            // Add item to inventory
            player.inventory.AddItemToInventory(gameObject);
            player.AddEquipment(gameObject);
            Destroy(gameObject);
        }
    }
}
