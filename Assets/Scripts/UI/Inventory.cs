using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Dictionary<string, InventoryItem> items = new Dictionary<string, InventoryItem>();
    public GameObject itemPanel;
    public GameObject itemPrefab;
    public void AddItemToInventory(GameObject itemToAdd)
    {
        string itemID = itemToAdd.GetComponent<Item>().id;
        if (items.ContainsKey(itemID))
        {
            items[itemID].AddToStack();
        }
        else
        {
            GameObject newItem = Instantiate(itemPrefab);
            newItem.GetComponent<InventoryItem>().SetInventoryItem(itemToAdd);
            items.Add(itemID, newItem.GetComponent<InventoryItem>());
            newItem.transform.SetParent(itemPanel.transform);
            newItem.transform.localScale = new Vector3(1, 1, 1);
        }
    }


    public void RemoveItemFromInventory(GameObject itemToRemove)
    {
        Destroy(items[itemToRemove.GetComponent<Item>().id].gameObject);
        items.Remove(itemToRemove.GetComponent<Item>().id);
    }
}
