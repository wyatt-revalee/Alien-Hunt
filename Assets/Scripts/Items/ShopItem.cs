using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


public class ShopItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Player player;
    public ItemData itemData;
    public Shop shop;
    public Item item;

    public Dictionary<int, Color> rarityColors = new Dictionary<int, Color>()
    {
        {1, new Color(1f, 1f, 1f, 0.2f)}, // Common, white
        {2, new Color(0.5f, 1f, 0.5f, 0.2f)}, // Uncommon, green
        {3, new Color(0.5f, 0.5f, 1f, 0.2f)}, // Rare, blue
        {4, new Color(1f, 0.5f, 1f, 0.2f)}, // Very Rare, pink
        {5, new Color(1f, 1f, 0f, 0.2f)}, // Legendary, yellow
    };

    public void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }
    
    public void UpdateItemInfo(Item newItem)
    {
        item = newItem;
        itemData = newItem.itemData;
        // Update the UI with the item's information
        transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = itemData.description;
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = itemData.itemName;
        transform.GetChild(3).GetComponent<Image>().sprite = itemData.image;
        transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = itemData.cost.ToString();
        SetRarityColor();
    }

    public void SetItemPurchased()
    {
        if(player.coins < itemData.cost)
        {
            return;
        }
        Destroy(gameObject);
        Destroy(this);
    }

    private void SetRarityColor()
    {
        transform.GetChild(2).GetComponent<Image>().color = rarityColors[itemData.rarity];
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowDescription();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideDescription();
    }

    public void ShowDescription()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(3).gameObject.SetActive(false);
    }

    public void HideDescription()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(true);
    }

    public void OnClick()
    {
        shop.PurchaseItem(item);
    }

}
