using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDisplayPanel : MonoBehaviour
{
    public WeaponController weaponController;
    private void Awake()
    {
        weaponController.OnNewWeaponSet += NewWeaponSet;
    }

    private void NewWeaponSet()
    {
        // Set the weapon display panel to the current weapon's sprite
        transform.GetChild(1).GetComponent<Image>().sprite = weaponController.currentWeaponScript.weaponSprite;
    }
}
