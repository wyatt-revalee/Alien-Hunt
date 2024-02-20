using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class TwinBarrelRevolver : Weapon
{
    public override void Shoot()
    {
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

            GameObject newBullet = Instantiate(bullet, new Vector3(transform.position.x + direction, transform.position.y, transform.position.z), Quaternion.identity);
            newBullet.GetComponent<Bullet>().weapon = this;
        }
        // Place new bullet
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
