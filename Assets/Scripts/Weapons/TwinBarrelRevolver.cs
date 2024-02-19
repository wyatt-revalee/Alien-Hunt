using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwinBarrelRevolver : Weapon
{
    public override void Shoot()
    {
        int direction = 1;
        for(int i = 0; i < bulletsPerShot; i++)
        {
            if(direction > 0)
            {
                direction *= -1;
            }
            else
            {
                direction++;
            }

            GameObject newBullet = Instantiate(bullet, new Vector3(transform.position.x + direction, transform.position.y, transform.position.z), Quaternion.identity);
            newBullet.GetComponent<Bullet>().weapon = this;
        }
        // Place new bullet
        ShootWeaponEvent();
        ShakeCrosshair(1);
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
