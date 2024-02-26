using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Shop : MonoBehaviour
{

    public Player player;
    public int itemsInShop;
    public int refreshCost = 0;
    public TextMeshProUGUI refreshCostText;
    public WeaponController weaponController;
    public LevelManager levelManager;
    public List<ItemData> shopItems;
    public GameObject shopItemPrefab;

    Dictionary<int, double> rarityChances = new Dictionary<int, double>
    {
        { 1, 0.4 }, // common
        { 2, 0.3 }, // uncommon
        { 3, 0.15 }, // rare
        { 4, 0.1 }, // very rare
        { 5, 0.05 } // legendary
    };

    public void RefreshShop()
    {
        if(player.coins >= refreshCost)
        {
            player.RemoveCoins(refreshCost);
            refreshCost++;
            refreshCostText.text = "-" + refreshCost;
        }
        else
        {
            Debug.Log("Not enough coins");
            return;
        }
        foreach (Transform child in transform.GetChild(0))
        {
            Destroy(child.gameObject);
        }

        List<ItemData> currentItemsInShop = new List<ItemData>();

        for(int i = 0; i < itemsInShop; i++)
        {
            int rarity = GetRarity();
            ItemData item;
            while (true)
            {
                item = shopItems[Random.Range(0, shopItems.Count)];
                if (item.rarity == rarity && !currentItemsInShop.Contains(item))
                {
                    break;
                }
                rarity = GetRarity();
            }
            currentItemsInShop.Add(item);
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
                    player.AddUpgrade(item.itemData);
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
        refreshCost = 0;
    }

    private int GetRarity()
    {
        double randomNumber = Random.Range(0f, 1f);
        int rarity = 1; // default to common
        foreach (var kvp in rarityChances)
        {
            if (randomNumber < kvp.Value)
            {
                rarity = kvp.Key;
                break;
            }
            else
            {
                randomNumber -= kvp.Value;
            }
        }
        return rarity;
    }

}
