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
    Dictionary<GameObject, int> effectsToAdd;

    public void SetBulletStats(float newSpeed, float newDamage, float newSizeMod, Dictionary<GameObject, int> effects)
    {
        speed = newSpeed;
        damage = newDamage;
        sizeModifier = newSizeMod;

        transform.localScale *= sizeModifier;

        effectsToAdd = new Dictionary<GameObject, int>(effects);
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
            string hitStatus = collider.GetComponent<Enemy>().TakeDamage((int)damage);
            foreach(KeyValuePair<GameObject, int> kvp in effectsToAdd)
            {
                GameObject i_effect = Instantiate(kvp.Key, collider.gameObject.transform);
                i_effect.GetComponent<StatusEffect>().stacks = kvp.Value;
                i_effect.GetComponent<StatusEffect>().InitializeEffects();
                i_effect.GetComponent<StatusEffect>().applier = owner;
                collider.GetComponent<StatusEffectSystem>().AddStatusEffect(i_effect.GetComponent<StatusEffect>());
            }
            collider.GetComponent<Enemy>().TakeDamage((int)damage);
            bool enemyKilled = false;
            if(hitStatus == "none")
            {
                owner.GetComponent<Player>().CallShotHit(false);
                Destroy(gameObject);
                return;
            }
            int pointsEarned = 0;
            if(hitStatus == "kill")
            {
                pointsEarned = (int)collider.GetComponent<Enemy>().GetAttributeValue("pointValue");
                enemyKilled = true;
            }
            owner.GetComponent<Player>().CallShotHit(true, enemyKilled, pointsEarned);
            Destroy(gameObject);
        }
        else if(collider.gameObject.layer == 10)
        {
            if(collider.GetComponent<KillZone>().blockBullets)
            {
                owner.GetComponent<Player>().CallShotHit(false);
                Destroy(gameObject);
            }
        }
    }
}
