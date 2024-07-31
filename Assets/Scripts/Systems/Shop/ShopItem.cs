using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ShopItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI descriptionText;
    public Item item;
    public Shop shop;
    public int rarity;

    public Dictionary<int, Color> rarityColors = new Dictionary<int, Color>()
    {
        {1, new Color(1f, 1f, 1f, 0.2f)}, // Common, white
        {2, new Color(0.5f, 1f, 0.5f, 0.2f)}, // Uncommon, green
        {3, new Color(0.5f, 0.5f, 1f, 0.2f)}, // Rare, blue
        {4, new Color(1f, 0.5f, 1f, 0.2f)}, // Very Rare, pink
        {5, new Color(1f, 1f, 0f, 0.2f)}, // Legendary, yellow
    };

    public void SetItemInfo(Item itemInfo)
    {
        transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = itemInfo.icon;
        nameText.text = itemInfo.itemName;
        costText.text = itemInfo.cost.ToString();
        descriptionText.text = itemInfo.description;
        rarity = itemInfo.rarity;
        GetComponent<Image>().color = rarityColors[rarity];
        item = itemInfo;

    }

    public void PurchaseItem()
    {
        shop.PurchaseItem(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(true);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(2).gameObject.SetActive(false);
    }
}
