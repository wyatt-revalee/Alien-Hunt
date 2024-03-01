using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItem", menuName = "ShopItems/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public string type;
    public int rarity;
    public Sprite image;
    public int cost;
    public string description;
    public string upgradeType;
    public float upgradeValue;
    public GameObject weapon;
    public GameObject item;
}
