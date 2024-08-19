using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{

    public Action onDeath;
    public AttributeSystem attributeSystem;
    public GameObject parentSpawner;
    public EnemyBullet bullet;
    public int index;
    public int cost = 1;
    public bool isMoving;
    public int horizontalDirection;
    public int verticalDirection;
    public bool enteredBarrier;
    public bool moveSignalSent;
    public bool isBoss;
    public virtual void Awake()
    {
        attributeSystem = GetComponent<AttributeSystem>();
        attributeSystem.attributes = new Dictionary<string, Attribute>
        {
            {"health", new Attribute("health", 1, 99, 1.0f, 0)},
            {"speed", new Attribute("speed", 10, 99, 1.0f, 0)},
            {"defense", new Attribute("defense", 0, 99, 1.0f, 0)},
            {"pointValue", new Attribute("pointValue", 10, 1000, 1.0f, 0)},
            {"shootDelay", new Attribute("shootDelay", 1, 10, 1.0f, 0)},
            {"bulletSpeed", new Attribute("bulletSpeed", 10, 99, 1.0f, 0)},
            {"damageModifier", new Attribute("damageModifier", 1, 99, 1.0f, 0)},
            {"bulletSizeModifier", new Attribute("bulletSizeModifier", 1, 99, 1.0f, 0)},
        };
    }

    public virtual void Update()
    {
        if(isMoving)
        {
            float speed = GetAttributeValue("speed") > 0 ? GetAttributeValue("speed") : 0.1f;
            GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalDirection * speed, 0);
        }
    }

    public float GetAttributeValue(string attribute)
    {
        return GetComponent<AttributeSystem>().attributes[attribute].GetTrueValue();
    }

    public virtual string TakeDamage(int damage)
    {
        if(GetAttributeValue("health") <= 0)
        {
            return "none";
        }

        attributeSystem.attributes["health"].delta -= damage;
        //Debug.Log("Took " + damage + " damage!");
        //Debug.Log("Current health: " + GetAttributeValue("health"));

        if(GetAttributeValue("health") <= 0)
        {
            isMoving = false;
            StopAllCoroutines();
            StartCoroutine(DeathSequence());
            return "kill";
        }
        return "hit";
    }

    public virtual void StartMovement(int horizontal = 0, int vertical = 0)
    {
        horizontalDirection = horizontal;
        verticalDirection = vertical;
        isMoving = true;
        StartCoroutine(StartShooting());
    }

    public virtual IEnumerator StartShooting()
    {
        while(true)
        {
            yield return new WaitForSeconds(GetAttributeValue("shootDelay"));
            EnemyBullet newBullet = Instantiate(bullet, transform.position, quaternion.identity);
            newBullet.SetBulletStats(GetAttributeValue("bulletSpeed"), GetAttributeValue("damageModifier"), GetAttributeValue("bulletSizeModifier"));
            newBullet.StartMovement(Vector2.down);
        }
    }

    public virtual IEnumerator DeathSequence()
    {
        onDeath?.Invoke();
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(1f);
        parentSpawner.GetComponent<EnemySpawner>().EnemyDied();
        Destroy(gameObject);
        Destroy(this);
    }
}
