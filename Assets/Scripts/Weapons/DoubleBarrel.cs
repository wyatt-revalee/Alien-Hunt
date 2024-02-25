using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class DoubleBarrel : Weapon
{
    public override void Shoot()
    {
        bulletsInMagazine -= bulletsPerShot;
        float spread = 0.3f;

        // Iterates through all bullets, spawning them with a spread. Ex: [1, -1, 2, -2,... n, -n]
        for (int bulletNum = 1; bulletNum <= bulletsPerShot; bulletNum++)
        {
            float direction;

            if(bulletNum % 2 == 0)
            {
                direction = -(bulletNum/2) * spread;
            }
            else
            {
                direction = ((bulletNum / 2)+1) *spread;
            }

            Vector3 location = new Vector3(transform.position.x + direction, transform.position.y, transform.position.z);

            // Place new bullet
            SpawnBullet(location);
        }
        ShakeCrosshair(1);
        ShakeCamera(1);
    }

    public override void UpdateBulletInfo(bool enemyHit, int pointsGained)
    {
        ShootWeaponEvent();
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
