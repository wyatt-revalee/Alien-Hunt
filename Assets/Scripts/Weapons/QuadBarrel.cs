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
        float spread = 0.3f;

        // Iterates through all bullets, spawning them with a spread as defined by bulletLocations
        for (int bulletNum = 1; bulletNum <= bulletsPerShot; bulletNum++)
        {
            Vector2 direction = bulletLocations[bulletNum-1];
            GameObject newBullet = Instantiate(bullet, new Vector3(transform.position.x + direction.x, transform.position.y + direction.y, transform.position.z), Quaternion.identity);
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
