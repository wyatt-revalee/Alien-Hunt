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

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.layer == 9)
        {
            collider.GetComponent<Enemy>().TakeDamage((int)damage);
            Destroy(gameObject);
            Destroy(this);
        }
    }
}
