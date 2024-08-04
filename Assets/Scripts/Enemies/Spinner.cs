using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class Spinner : Enemy
{

    Rigidbody2D rb;
    private float timeToChangeDirection = 1f;
    private float circleRadius = 10f;
    private float circleSpeed = 2f * Mathf.PI; // One full circle per second

    public override void Awake()
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

        rb = GetComponent<Rigidbody2D>();
    }

    public override void Update()
    {
        if (isMoving && !moveSignalSent)
        {
            StartCoroutine(MoveAcrossScreen());
            moveSignalSent = true;
        }
        if (!isMoving)
        {
            StopCoroutine(MoveAcrossScreen());
            rb.velocity = new Vector2(0, 0);
        }
    }


    private IEnumerator MoveAcrossScreen()
    {
        while (isMoving)
        {
            // Move forward for a second
            rb.velocity = new Vector2(GetAttributeValue("speed") * horizontalDirection, 0);
            yield return new WaitForSeconds(timeToChangeDirection);

            // Do a circular pattern for a second
            float startTime = Time.time;
            while (Time.time < startTime + timeToChangeDirection)
            {
                float t = Time.time - startTime;
                float x = circleRadius * Mathf.Cos(circleSpeed * t);
                float y = circleRadius * Mathf.Sin(circleSpeed * t);
                rb.velocity = new Vector2(x, y);
                yield return null;
            }
        }
    }
}
