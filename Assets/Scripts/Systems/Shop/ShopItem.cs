using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ShopItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI descriptionText;
    public Item item;
    public Shop shop;

    public void SetItemInfo(Item itemInfo)
    {
        transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = itemInfo.icon;
        nameText.text = itemInfo.itemName;
        costText.text = itemInfo.cost.ToString();
        descriptionText.text = itemInfo.description;
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
