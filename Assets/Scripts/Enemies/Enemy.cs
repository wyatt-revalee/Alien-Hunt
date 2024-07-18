using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public AttributeSystem attributeSystem;
    public int cost = 1;
    // Start is called before the first frame update
    public virtual void Awake()
    {
        attributeSystem = GetComponent<AttributeSystem>();
        attributeSystem.attributes = new Dictionary<string, Attribute>
        {
            {"health", new Attribute("health", 1, 99, 1.0f, 0)},
            {"speed", new Attribute("speed", 1, 99, 1.0f, 0)},
            {"defense", new Attribute("defense", 0, 99, 1.0f, 0)},
            {"pointValue", new Attribute("pointValue", 10, 1000, 1.0f, 0)}
        };
    }

    public float GetAttributeValue(string attribute)
    {
        return GetComponent<AttributeSystem>().attributes[attribute].GetTrueValue();
    }

    public virtual void TakeDamage(int damage)
    {
        attributeSystem.attributes["health"].delta -= damage;
        Debug.Log("Took " + damage + " damage!");
        Debug.Log("Current health: " + GetAttributeValue("health"));

        if(GetAttributeValue("health") <= 0)
        {
            StartCoroutine(DeathSequence());
        }


    }

    public virtual IEnumerator DeathSequence()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        Destroy(this);
    }
}
