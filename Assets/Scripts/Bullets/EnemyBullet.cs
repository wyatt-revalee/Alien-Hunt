using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    float speed;
    float damage;
    float sizeModifier;

    public void SetBulletStats(float newSpeed, float newDamage, float newSizeMod)
    {
        speed = newSpeed;
        damage = newDamage;
        sizeModifier = newSizeMod;

        transform.localScale *= sizeModifier;
    }

    public void StartMovement()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.down * speed;
    }

    // If colliding object is an enemy, then hurt it and destroy bullet
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == 6 && collider.GetComponent<Player>())
        {
            collider.GetComponent<Player>().Damage((int)damage);
            Destroy(gameObject);
        }
    }
}
