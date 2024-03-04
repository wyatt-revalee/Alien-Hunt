using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySplitShot : EnemyBullet
{
    public override IEnumerator DoMovement()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1 * (bulletSpeed * enemy.bulletSpeedModifier));
        }
    }
}
