using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;


public class WeaponSlot : MonoBehaviour
{
    public TextMeshProUGUI slotNumber;
    public GameObject weaponImage;
    public WeaponSelector weaponSelector;

    public void SetSlotNumber(int slotNum)
    {
        slotNumber.text = slotNum.ToString();
    }

    public void SetWeaponImage(Sprite weaponSprite)
    {
        weaponImage.GetComponent<Image>().sprite = weaponSprite;
    }

    public void SetWeaponAsActive()
    {
        
        //Debug.Log(Int32.Parse(slotNumber.text));
        weaponSelector.SetSelectedWeapon(Int32.Parse(slotNumber.text));
    }
}
