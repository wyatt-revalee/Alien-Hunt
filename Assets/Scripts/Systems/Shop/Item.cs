using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Sprite icon;
    public string itemName;
    public string type;
    public string id;
    public string description;
    public int cost;
    public int rarity;
    public GameObject statusEffect;
    public GameObject activeEquipment;
}

// Rarity Chart
//  1  common
//  2  uncommon
//  3  rare
//  4  very rare
//  5  legendary