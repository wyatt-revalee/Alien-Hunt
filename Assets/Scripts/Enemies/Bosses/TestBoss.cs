using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class TestBoss : Enemy
{

    public override void Awake()
    {
        attributeSystem = GetComponent<AttributeSystem>();
        attributeSystem.attributes = new Dictionary<string, Attribute>
        {
            {"health", new Attribute("health", 100, 99, 1.0f, 0)},
            {"speed", new Attribute("speed", 5, 99, 1.0f, 0)},
            {"defense", new Attribute("defense", 0, 99, 1.0f, 0)},
            {"pointValue", new Attribute("pointValue", 10, 1000, 1.0f, 0)},
            {"shootDelay", new Attribute("shootDelay", 1, 10, 1.0f, 0)},
            {"bulletSpeed", new Attribute("bulletSpeed", 10, 99, 1.0f, 0)},
            {"damageModifier", new Attribute("damageModifier", 1, 99, 1.0f, 0)},
            {"bulletSizeModifier", new Attribute("bulletSizeModifier", 1, 99, 1.0f, 0)},
        };
    }

    public override void StartMovement(int horizontal = 0, int vertical = 0) // Moves boss to area where it will begin its attacking
    {
        StartCoroutine(MoveToStartPosition());
    }

    public IEnumerator MoveToStartPosition()
    {
        while (transform.position.y > 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetAttributeValue("speed") * -1);
            if (transform.position.y <= 0)
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
            yield return null;
        }
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public override IEnumerator DeathSequence()
    {
        onDeath?.Invoke();
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        Destroy(this);
    }

    public override void Update()
    {

    }

}
