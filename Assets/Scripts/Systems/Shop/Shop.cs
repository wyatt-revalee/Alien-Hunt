using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    public Action onShopEnd;
    public Player player;
    public int itemsInShop;
    public int refreshCost = 0;
    public TextMeshProUGUI refreshCostText;
    public List<GameObject> shopItems;
    public GameObject shopItemPrefab;
    public GameObject itemHolder;

    Dictionary<int, double> rarityChances = new Dictionary<int, double>
    {
        { 1, 0.4 }, // common
        { 2, 0.3 }, // uncommon
        { 3, 0.15 }, // rare
        { 4, 0.1 }, // very rare
        { 5, 0.05 } // legendary
    };
    public void ExitShop()
    {
        onShopEnd?.Invoke();
        gameObject.SetActive(false);
    }

    public void BuyItem(GameObject shopItem)
    {   
        Debug.Log(string.Format("Item {0} bought!", shopItem.name));
    }

    public void RefreshShop()
    {
        if (player.coins >= refreshCost)
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
        foreach (Transform child in itemHolder.transform)
        {
            Destroy(child.gameObject);
        }

        List<Item> currentItemsInShop = new List<Item>();

        for (int i = 0; i < itemsInShop; i++)
        {
            int rarity = GetRarity();
            Item item;
            // while (true)
            // {
            //     item = shopItems[UnityEngine.Random.Range(0, shopItems.Count)].GetComponent<Item>();
            //     if (item.rarity == rarity && !currentItemsInShop.Contains(item))
            //     {
            //         break;
            //     }
            //     rarity = GetRarity();
            // }
            item = shopItems[UnityEngine.Random.Range(0, shopItems.Count)].GetComponent<Item>();
            currentItemsInShop.Add(item);
            GameObject newItem = Instantiate(shopItemPrefab, transform);
            newItem.GetComponent<ShopItem>().SetItemInfo(item);
            newItem.GetComponent<ShopItem>().shop = this;
            newItem.transform.SetParent(itemHolder.transform);
        }

    }

    public void PurchaseItem(ShopItem shopItem)
    {
        Item item = shopItem.item;
        if (player.coins >= item.cost)
        {
            Debug.Log("Purchased " + item.itemName);
            player.RemoveCoins(item.cost);
            switch (item.type)
            {
                case "Status Effect":
                    GameObject i_item = Instantiate(item.statusEffect, player.transform);
                    i_item.GetComponent<StatusEffect>().InitializeEffects();
                    player.GetComponent<StatusEffectSystem>().AddStatusEffect(i_item.GetComponent<StatusEffect>());
                    break;
            }
            player.inventory.AddItemToInventory(item.gameObject);
            Destroy(shopItem.gameObject);
        }
        else
        {
            Debug.Log("Not enough coins");
        }
    }

    private int GetRarity()
    {
        double randomNumber = UnityEngine.Random.Range(0f, 1f);
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
