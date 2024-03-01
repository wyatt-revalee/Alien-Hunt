using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public abstract class Bullet : MonoBehaviour
{

    public LayerMask enemyMask;
    public int pointsGained;
    public bool enemyHit;
    public Weapon weapon;
    public Sprite magazineSprite;

    private void Start()
    {
        StartCoroutine(DoMovement());
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // If object is an enemy
        if(collider.transform.gameObject.layer == 6)
        {
            Enemy enemy = collider.transform.gameObject.GetComponent<Enemy>();
            enemyHit = true;
            if(enemy.health > 0)
            {
                enemy.Damage((int)((weapon.damage + weapon.player.damageModiferFlat.value) * weapon.player.damageModifierPercentage.value));
                if(enemy.health <= 0)
                {
                    pointsGained = enemy.pointValue;
                }
            }
            Destroy(gameObject);
        }
        weapon.UpdateBulletInfo(enemyHit, pointsGained);
    }

    private IEnumerator DoMovement()
    {
        while (true)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1 *(weapon.bulletSpeed * weapon.player.bulletSpeedModifier.value));
            yield return new WaitForSeconds(0.01f);
        }
    }

}
