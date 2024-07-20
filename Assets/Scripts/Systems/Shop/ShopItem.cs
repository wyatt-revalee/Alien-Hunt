using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI costText;
    public Item item;
    public Shop shop;

    public void SetItemInfo(Item itemInfo)
    {
        transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = itemInfo.icon;
        nameText.text = itemInfo.itemName;
        costText.text = itemInfo.cost.ToString();
        item = itemInfo;
    }

    public void PurchaseItem()
    {
        shop.PurchaseItem(this);
    }
}
