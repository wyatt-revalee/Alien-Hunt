using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


public class WeaponSlot : MonoBehaviour
{
    public TextMeshProUGUI slotNumber;
    public GameObject weaponImage;

    public void SetSlotNumber(int slotNum)
    {
        slotNumber.text = slotNum.ToString();
    }

    public void SetWeaponImage(Sprite weaponSprite)
    {
        weaponImage.GetComponent<Image>().sprite = weaponSprite;
    }
}
