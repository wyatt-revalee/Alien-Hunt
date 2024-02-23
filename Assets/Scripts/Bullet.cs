using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Bullet : MonoBehaviour
{

    public LayerMask enemyMask;
    public BulletData bulletData;
    public int pointsGained;
    public bool enemyHit;
    public Weapon weapon;
    public Sprite magazineSprite;

    private void Start()
    {
        StartCoroutine(DecayAndDestroy());
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
                enemy.Damage(bulletData.damage + weapon.damage);
                if(enemy.health <= 0)
                {
                    pointsGained = enemy.pointValue;
                }
            }
            Destroy(gameObject);
        }
        weapon.UpdateBulletInfo(enemyHit, pointsGained);
    }

    public IEnumerator DecayAndDestroy()
    {
        yield return new WaitForFixedUpdate();
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        weapon.UpdateBulletInfo(false, 0);
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = new Color(184, 180, 0);
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = new Color(123, 0, 180);
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = new Color(62, 180, 180);
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
