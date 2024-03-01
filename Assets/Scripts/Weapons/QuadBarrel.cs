using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class QuadBarrel : Weapon
{

    public List<Vector2> bulletLocations = new List<Vector2>
    {
        new Vector2(0.3f, 0.3f),
        new Vector2(-0.3f, 0.3f),
        new Vector2(0.3f, -0.3f),
        new Vector2(-0.3f, -0.3f)
    };
    public override void Shoot()
    {
        bulletsInMagazine -= bulletsPerShot;
        for (int bulletNum = 1; bulletNum <= bulletsPerShot; bulletNum++)
        {
            Vector2 direction = bulletLocations[bulletNum-1];
            Vector3 location = new Vector3(transform.position.x + direction.x, transform.position.y + direction.y, transform.position.z);
            SpawnBullet(location);
        }
        // Place new bullet
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
