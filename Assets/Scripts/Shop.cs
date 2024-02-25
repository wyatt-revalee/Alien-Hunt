using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shop : MonoBehaviour
{

    public Player player;
    public WeaponController weaponController;
    public LevelManager levelManager;
    public List<ItemData> shopItems;
    public GameObject shopItemPrefab;
    

    public void RefreshShop()
    {
        foreach (Transform child in transform.GetChild(0))
        {
            Destroy(child.gameObject);
        }
        foreach(ItemData item in shopItems)
        {
            GameObject newItem = Instantiate(shopItemPrefab, transform);
            newItem.GetComponent<ShopItem>().UpdateItemInfo(item);
            newItem.transform.SetParent(transform.GetChild(0));
            newItem.GetComponent<ShopItem>().player = player;
            newItem.GetComponent<ShopItem>().shop = this;
        }

    }

    public void PurchaseItem(ShopItem item)
    {
        if(player.coins >= item.itemData.cost)
        {
            Debug.Log("Purchased " + item.itemData.itemName);
            player.RemoveCoins(item.itemData.cost);
            switch (item.itemData.type)
            {
                case "Upgrade":
                    player.AddUpgrade(item.itemData.upgradeType, item.itemData.upgradeValue);
                    break;
                case "Weapon":
                    weaponController.SetNewWeapon(item.itemData.weapon);
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
