using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPickup : MonoBehaviour
{

    public GameObject equipmentItem;

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.layer == 6)
        {
            Player player = collider2D.GetComponent<Player>();

            // Instantiate and equipment to inventory
            GameObject i_Equipment = Instantiate(equipmentItem);
            player.inventory.AddItemToInventory(i_Equipment);
            player.AddEquipment(i_Equipment);
            Destroy(gameObject);
        }
    }
}
