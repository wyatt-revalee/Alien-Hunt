using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : Weapon
{
    public override void Shoot()
    {
        if(currentAmmo < 0)
        {
            return;
        }
        bulletsInMagazine--;
        currentAmmo--;
        // Place new bullet
        SpawnBullet(transform.position);
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
