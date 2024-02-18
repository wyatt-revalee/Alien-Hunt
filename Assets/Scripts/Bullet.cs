using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Bullet : MonoBehaviour
{

    public LayerMask enemyMask;
    public BulletData bulletData;
    public int pointsGained;
    public bool enemyHit;
    public Crosshair crosshair;

    private void Start()
    {
        StartCoroutine(DecayAndDestroy());
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // If object is an enemy
        if(collider.transform.gameObject.layer == 6)
        {
            enemyHit = true;
            Enemy enemy = collider.transform.gameObject.GetComponent<Enemy>();
            enemy.Damage(bulletData.damage);
            if(enemy.health <= 0)
            {
                pointsGained = enemy.pointValue;
            }

            Destroy(gameObject);
            crosshair.UpdateBulletInfo(enemyHit, pointsGained);
        }
    }

    public IEnumerator DecayAndDestroy()
    {
        yield return new WaitForSeconds(0.1f);
        crosshair.UpdateBulletInfo(false, 0);
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
