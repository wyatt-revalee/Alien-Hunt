using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shop : MonoBehaviour
{

    public Player player;
    public LevelManager levelManager;
    

    public void PurchaseItem(ShopItem item)
    {
        if(player.coins >= item.itemData.cost)
        {
            Debug.Log("Purchased " + item.itemData.itemName);
            player.coins -= item.itemData.cost;
            switch (item.itemData.type)
            {
                case "Upgrade":
                    player.AddUpgrade(item.itemData.upgradeType, item.itemData.upgradeValue);
                    break;
                case "Weapon":
                    break;
            }
        }
        else
        {
            Debug.Log("Not enough coins");
        }
    }

    public void CloseShop()
    {
        gameObject.SetActive(false);
        levelManager.isShopping = false;
    }

}
