using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class WeaponSelector : MonoBehaviour
{

    public PlayerController playerController;
    public GameObject weaponSlot;
    public bool weaponSelected = false;

    public void Awake()
    {
        playerController.OnWeaponsUpdated += UpdateWeaponSlots;
    }

    public void Start()
    {
        UpdateWeaponSlots();
    }

    public void UpdateWeaponSlots()
    {

        //Debug.Log("Updating weapon slots");

        // Clear out weapons
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Add back current ones
        int weaponCount = 1;
        foreach(StoredWeaponInfo weapon in playerController.weapons)
        {
            GameObject newWeaponSlot = Instantiate(weaponSlot, transform.position, Quaternion.identity);
            newWeaponSlot.GetComponent<WeaponSlot>().SetSlotNumber(weaponCount);
            newWeaponSlot.GetComponent<WeaponSlot>().SetWeaponImage(weapon.weaponObject.GetComponent<Weapon>().weaponSprite);
            newWeaponSlot.GetComponent<WeaponSlot>().weaponSelector = this;
            newWeaponSlot.transform.SetParent(transform);
            newWeaponSlot.transform.localScale = new Vector3(1, 1, 1);
            weaponCount++;
        }

    }

    public void SetSelectedWeapon(int slotNumber)
    {
        playerController.SetNewWeapon(playerController.weapons[slotNumber-1].weaponObject);
        gameObject.SetActive(false);
        playerController.isPaused = false;
        Time.timeScale = 1f;
    }

    public void StartWeaponSelection()
    {
        Time.timeScale = 0f;
        StartCoroutine(WaitForWeaponSelection());
    }

    public IEnumerator WaitForWeaponSelection()
    {
        weaponSelected = false;
        while (!weaponSelected)
        {
            yield return new WaitForSeconds(0f);
        }
        gameObject.SetActive(false);
    }

}
