using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string itemName;
    public string itemDescription;

    public void SetItemText()
    {
        transform.GetChild(2).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = itemName + "\n" + itemDescription;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(true);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(2).gameObject.SetActive(false);
    }
}
