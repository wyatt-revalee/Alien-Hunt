using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPickup : MonoBehaviour
{

    public GameObject equipmentItem;

    // Upon entering collider of pickup, check what type of item this is, and add to player accordingly
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.layer == 6)
        {
            Player player = collider2D.GetComponent<Player>();

            Item item = equipmentItem.GetComponent<Item>();
            switch (item.type)
            {
                case "Status Effect":
                    GameObject i_statusEffect = Instantiate(item.statusEffect, player.transform);
                    i_statusEffect.GetComponent<StatusEffect>().InitializeEffects();
                    player.GetComponent<StatusEffectSystem>().AddStatusEffect(i_statusEffect.GetComponent<StatusEffect>());
                    break;

                case "Bullet Effect":
                    player.GetComponent<Player>().AddBulletEffect(item.statusEffect);
                    break;

    
                case "Active Equipment":
                    player.AddEquipment(item.gameObject);
                    break;
            }
            player.inventory.AddItemToInventory(item.gameObject);

            Destroy(gameObject);
        }
    }
}
