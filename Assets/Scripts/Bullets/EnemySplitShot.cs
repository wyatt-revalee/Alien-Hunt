using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySplitShot : EnemyBullet
{
    public override IEnumerator DoMovement()
    {
        yield return new WaitForSeconds(0f);
        transform.GetChild(0).GetComponent<Rigidbody2D>().velocity = new Vector2(-0.66f * (bulletSpeed * enemy.bulletSpeedModifier), -0.33f * (bulletSpeed * enemy.bulletSpeedModifier));
        transform.GetChild(1).GetComponent<Rigidbody2D>().velocity = new Vector2(-0.33f * (bulletSpeed * enemy.bulletSpeedModifier), -0.66f * (bulletSpeed * enemy.bulletSpeedModifier));
        transform.GetChild(2).GetComponent<Rigidbody2D>().velocity = new Vector2(0 * (bulletSpeed * enemy.bulletSpeedModifier), -1f * (bulletSpeed * enemy.bulletSpeedModifier));
        transform.GetChild(3).GetComponent<Rigidbody2D>().velocity = new Vector2(0.33f * (bulletSpeed * enemy.bulletSpeedModifier), -0.66f * (bulletSpeed * enemy.bulletSpeedModifier));
        transform.GetChild(4).GetComponent<Rigidbody2D>().velocity = new Vector2(0.66f * (bulletSpeed * enemy.bulletSpeedModifier), -0.33f * (bulletSpeed * enemy.bulletSpeedModifier));
    }
}
