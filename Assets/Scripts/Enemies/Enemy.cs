using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public AttributeSystem attributeSystem;
    public GameObject parentSpawner;
    public int index;
    public int cost = 1;
    public virtual void Awake()
    {
        attributeSystem = GetComponent<AttributeSystem>();
        attributeSystem.attributes = new Dictionary<string, Attribute>
        {
            {"health", new Attribute("health", 1, 99, 1.0f, 0)},
            {"speed", new Attribute("speed", 10, 99, 1.0f, 0)},
            {"defense", new Attribute("defense", 0, 99, 1.0f, 0)},
            {"pointValue", new Attribute("pointValue", 10, 1000, 1.0f, 0)}
        };
    }

    public float GetAttributeValue(string attribute)
    {
        return GetComponent<AttributeSystem>().attributes[attribute].GetTrueValue();
    }

    public virtual bool TakeDamage(int damage)
    {
        attributeSystem.attributes["health"].delta -= damage;
        //Debug.Log("Took " + damage + " damage!");
        //Debug.Log("Current health: " + GetAttributeValue("health"));

        if(GetAttributeValue("health") <= 0)
        {
            StartCoroutine(DeathSequence());
            return true;
        }
        return false;
    }

    public virtual void StartMovement(int direction)
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(direction * GetAttributeValue("speed"), 0);
    }

    public virtual IEnumerator DeathSequence()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(1f);
        parentSpawner.GetComponent<EnemySpawner>().EnemyDied();
        Destroy(gameObject);
        Destroy(this);
    }
}
