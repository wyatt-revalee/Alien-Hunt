using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject owner;
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
        GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
    }

    // If colliding object is an enemy, then hurt it and destroy bullet
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.layer == 9)
        {
            bool enemyKilled = collider.GetComponent<Enemy>().TakeDamage((int)damage);
            int pointsEarned = 0;
            if(enemyKilled)
            {
                pointsEarned = (int)collider.GetComponent<Enemy>().GetAttributeValue("pointValue");
            }
            owner.GetComponent<Player>().CallShotHit(true, enemyKilled, pointsEarned);
            Destroy(gameObject);
            Destroy(this);
        }
        else if(collider.gameObject.layer == 10)
        {
            owner.GetComponent<Player>().CallShotHit(false);
        }
    }
}
