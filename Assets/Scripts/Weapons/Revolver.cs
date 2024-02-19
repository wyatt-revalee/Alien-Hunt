using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : Weapon
{
    public override void Shoot()
    {
        // Place new bullet
        GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        newBullet.GetComponent<Bullet>().weapon = this;
        ShootWeaponEvent();
        ShakeCrosshair(1);
        ShakeCamera(1);
    }

}
