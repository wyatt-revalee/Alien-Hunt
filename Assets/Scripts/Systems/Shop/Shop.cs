using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public List<GameObject> items;
    public Action onShopEnd;
    public void ExitShop()
    {
        onShopEnd?.Invoke();
        gameObject.SetActive(false);
    }

    public void BuyItem(GameObject shopItem)
    {   
        Debug.Log(string.Format("Item {0} bought!", shopItem.name));
    }
}
