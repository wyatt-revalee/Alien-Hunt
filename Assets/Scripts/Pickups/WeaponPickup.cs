using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameObject weapon;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.gameObject.layer == 8)
        {
            // Grab the parent weapon of the bullet, and then the weapon controller of that weapon
            WeaponController weaponController = collider.transform.gameObject.GetComponent<Bullet>().weapon.transform.parent.GetComponent<WeaponController>();
            weaponController.SetNewWeapon(weapon);
            Destroy(gameObject);
        }
    }

}
