using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDisplayPanel : MonoBehaviour
{
    public PlayerController playerController;
    public TextMeshProUGUI ammoCount;
    private void Awake()
    {
        playerController.OnNewWeaponSet += NewWeaponSet;
        playerController.OnWeaponFired += UpdateAmmoCount;
    }

    private void NewWeaponSet()
    {
        // Set the weapon display panel to the current weapon's sprite
        transform.GetChild(1).GetComponent<Image>().sprite = playerController.currentWeaponScript.weaponSprite;
        ammoCount.text = playerController.currentWeaponScript.currentAmmo.ToString() + '/' + playerController.currentWeaponScript.ammoCapacity.ToString();
    }

    private void UpdateAmmoCount()
    {
        ammoCount.text = playerController.currentWeaponScript.currentAmmo.ToString() + '/' + playerController.currentWeaponScript.ammoCapacity.ToString();
    }
}
