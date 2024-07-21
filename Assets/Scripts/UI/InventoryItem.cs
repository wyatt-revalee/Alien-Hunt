using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;


public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject item;
    public int stackSize;
    public TextMeshProUGUI stackText;
    public TextMeshProUGUI descriptionText;
    public UnityEngine.UI.Image icon;

    public void SetInventoryItem(GameObject newItem)
    {
        item = newItem;
        stackSize = 1;
        stackText.text = stackSize.ToString();
        descriptionText.text = newItem.GetComponent<Item>().description;
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
