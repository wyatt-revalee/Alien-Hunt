using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Player player;
    public WeaponController weaponController;
    public GameObject playerStat;

    private void Awake()
    {
        player.OnUpgradeAdded += RefreshInventory;
        weaponController.OnNewWeaponSet += UpdateWeapon;
        RefreshInventory();
        gameObject.SetActive(false);
    
    }

    public void RefreshInventory()
    {
        Debug.Log("Refreshing Inventory");
        RefreshStats();
        RefreshItemDisplay();
    }

    public void RefreshStats()
    {
        foreach (Transform child in transform.GetChild(2).transform.GetChild(1))
        {
            Destroy(child.gameObject);
        }
        foreach (KeyValuePair<string, Stat> kvp in player.stats)
        {
            GameObject playerStatInstance = Instantiate(playerStat, transform);
            playerStatInstance.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = string.Format("{0}: {1}", kvp.Value.name, kvp.Value.value);
            playerStatInstance.transform.GetChild(1).GetComponent<Image>().sprite = kvp.Value.sprite;
            playerStatInstance.transform.SetParent(transform.GetChild(2).transform.GetChild(1));
        }
    }

    public void RefreshItemDisplay()
    {
        foreach (KeyValuePair<ItemData, int> kvp in player.inventory)
        {

        }
    }

    public void UpdateWeapon()
    {
        Debug.Log("Updating Weapon Image");
        transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = weaponController.currentWeaponScript.weaponSprite;
    }

}
