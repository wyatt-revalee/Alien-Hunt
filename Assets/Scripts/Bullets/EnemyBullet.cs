using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public abstract class EnemyBullet : MonoBehaviour
{
    public Enemy enemy;
    public int damage;
    public float bulletSpeed;
    public bool isMovingStraight = true;

    private void Start()
    {
        StartCoroutine(DoMovement());
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // If object is an enemy
        if(collider.transform.gameObject.layer == 7)
        {
            Player player = collider.transform.gameObject.GetComponent<Player>();
            if(player.health > 0)
            {
                player.Damage((int)(damage * enemy.damageModifier));
            }
            Destroy(gameObject);
        }
    }

    public virtual IEnumerator DoMovement()
    {
        while (isMovingStraight)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1 *(bulletSpeed * enemy.bulletSpeedModifier));
            yield return new WaitForSeconds(0.01f);
        }
    }

}
