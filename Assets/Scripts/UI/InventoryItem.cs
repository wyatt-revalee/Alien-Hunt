using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public GameObject item;
    public int stackSize;
    public TextMeshProUGUI stackText;
    public UnityEngine.UI.Image icon;

    public void SetInventoryItem(GameObject newItem)
    {
        item = newItem;
        stackSize = 1;
        stackText.text = stackSize.ToString();
        icon.sprite = newItem.GetComponent<Item>().icon;
    }

    public void AddToStack()
    {
        stackSize++;
        stackText.text = stackSize.ToString();
    }

    public void RemoveFromStack()
    {
        stackSize--;
        stackText.text = stackSize.ToString();
    }
}
