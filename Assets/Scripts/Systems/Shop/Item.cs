using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public Image image;
    public string itemName;
    public int cost;
    public string rarity;
    public GameObject statusEffect;
}
