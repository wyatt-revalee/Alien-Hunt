using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamGun : Weapon
{
    public override void Shoot()
    {
        bulletsInMagazine--;
        // Place new bullet
        GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        newBullet.GetComponent<Bullet>().weapon = this;
        ShootWeaponEvent();
        ShakeCamera(1);
    }

    public override void UpdateBulletInfo(bool enemyHit, int pointsGained)
    {
        if (enemyHit)
        {
            EnemyHitEvent();
        }
        if (pointsGained > 0)
        {
            EnemyKilledEvent(pointsGained);
        }
    }

}
