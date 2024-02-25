using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


public class ShopItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Player player;
    public ItemData itemData;
    public Shop shop;

    public void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }
    
    public void UpdateItemInfo(ItemData newItemData)
    {
        itemData = newItemData;
        // Update the UI with the item's information
        transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = itemData.description;
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = itemData.itemName;
        transform.GetChild(2).GetComponent<Image>().sprite = itemData.image;
        transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = itemData.cost.ToString();
    }

    public void SetItemPurchased()
    {
        Debug.Log(player.coins + " " + itemData.cost);
        if(player.coins < itemData.cost)
        {
            return;
        }
        Destroy(gameObject);
        Destroy(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(2).gameObject.SetActive(false);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(true);
    }

    public void OnClick()
    {
        shop.PurchaseItem(this);
    }

}