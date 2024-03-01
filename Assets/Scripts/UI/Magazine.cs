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
    public PlayerController playerController;

    private void Awake()
    {
        playerController.OnWeaponFired += UseBullet;
        playerController.OnWeaponReloaded += Reload;
        playerController.OnNewWeaponSet += SetNewWeapon;
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
        bulletSprite = playerController.currentWeaponScript.bullet.GetComponent<Bullet>().magazineSprite;
        magazineSize = (int)(playerController.currentWeaponScript.magazineSize * player.magazineSizeModifier.value) / playerController.currentWeaponScript.bulletsPerShot;
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
        playerController.currentWeaponScript.bulletsInMagazine = bulletsInMagazine;
    }

}
