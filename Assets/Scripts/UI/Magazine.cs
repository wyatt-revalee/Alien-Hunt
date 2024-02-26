using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Magazine : MonoBehaviour
{
    public int magazineSize;
    public int bulletsInMagazine;
    public Sprite bulletSprite;
    public Player player;
    public WeaponController weaponController;

    private void Awake()
    {
        weaponController.OnWeaponFired += UseBullet;
        weaponController.OnWeaponReloaded += Reload;
        weaponController.OnNewWeaponSet += SetNewWeapon;
        player.OnMagazineSizeChange += SetNewWeapon;
    }

    public void Reload()
    {
        bulletsInMagazine = magazineSize;
        for(int i = 0; i < magazineSize; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<Image>().color = new Color(255, 255, 255);
        }
    }

    public void UseBullet()
    {
        bulletsInMagazine--;
        transform.GetChild(bulletsInMagazine).gameObject.GetComponent<Image>().color = new Color(0, 0, 0);
    }

    public void SetNewWeapon()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        bulletSprite = weaponController.currentWeaponScript.bullet.GetComponent<Bullet>().magazineSprite;
        magazineSize = (int)(weaponController.currentWeaponScript.magazineSize * player.magazineSizeModifier) / weaponController.currentWeaponScript.bulletsPerShot;
        bulletsInMagazine = magazineSize;
        for (int i = 0; i < magazineSize; i++)
        {
            GameObject newBullet = new GameObject();
            newBullet.AddComponent<Image>();
            newBullet.GetComponent<Image>().sprite = bulletSprite;
            newBullet.transform.SetParent(transform);
            newBullet.transform.localPosition = new Vector3(0, 0, 0);
            newBullet.transform.localScale = new Vector3(1, 1, 1);
        }
        weaponController.currentWeaponScript.bulletsInMagazine = bulletsInMagazine;
    }

}
