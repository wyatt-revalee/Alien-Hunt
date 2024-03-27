using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelector : MonoBehaviour
{

    public PlayerController playerController;
    public GameObject weaponSlot;

    public void UpdateWeaponSlots()
    {
        int weaponCount = 1;
        foreach(GameObject weapon in playerController.weapons)
        {
            GameObject newWeaponSlot = Instantiate(weaponSlot, transform.position, Quaternion.identity);
            newWeaponSlot.GetComponent<WeaponSlot>().SetSlotNumber(weaponCount);
            newWeaponSlot.GetComponent<WeaponSlot>().SetWeaponImage(weapon.GetComponent<Weapon>().weaponSprite);
            weaponCount++;
        }
    }
}
