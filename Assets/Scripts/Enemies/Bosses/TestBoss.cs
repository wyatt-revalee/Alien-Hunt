using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using Unity.Mathematics;
public class TestBoss : Enemy
{

    public bool isPerformingSequence;
    public string currentSequence;
    bool atStartPosition;
    public List<string> sequences = new List<string>();
    public override void Awake()
    {
        isBoss = true;
        attributeSystem = GetComponent<AttributeSystem>();
        attributeSystem.attributes = new Dictionary<string, Attribute>
        {
            {"health", new Attribute("health", 100, 99, 1.0f, 0)},
            {"speed", new Attribute("speed", 10, 99, 1.0f, 0)},
            {"defense", new Attribute("defense", 0, 99, 1.0f, 0)},
            {"pointValue", new Attribute("pointValue", 10, 1000, 1.0f, 0)},
            {"shootDelay", new Attribute("shootDelay", 1, 1, 0.5f, 0)},
            {"bulletSpeed", new Attribute("bulletSpeed", 12, 99, 1.0f, 0)},
            {"damageModifier", new Attribute("damageModifier", 1, 99, 1.0f, 0)},
            {"bulletSizeModifier", new Attribute("bulletSizeModifier", 1, 99, 1.0f, 0)},
        };
    }

    public override void StartMovement(int horizontal = 0, int vertical = 0) // Moves boss to area where it will begin its attacking
    {
        if(atStartPosition)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x * -1, GetComponent<Rigidbody2D>().velocity.y * -1);
        }
        else
        {
            StartCoroutine(MoveToStartPosition());
        }
    }

    public IEnumerator DoMovement()
    {
         while(true)
         {
            if(isPerformingSequence)
            {
                yield return null;
            }
            currentSequence = sequences[UnityEngine.Random.Range(0, sequences.Count)];
            isPerformingSequence = true;
            Coroutine currentRoutine = StartCoroutine(PerformSequence(sequences.IndexOf(currentSequence)));
            Debug.Log(string.Format("Performing {0} sequence", currentSequence));
            yield return new WaitForSeconds(5);
            if(currentRoutine != null)
            {
                StopCoroutine(currentRoutine);
            }
            isPerformingSequence = false;
        }

    }

    IEnumerator PerformSequence(int sequenceNumber)
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //Debug.Log(sequenceNumber);
        switch (sequenceNumber)
        {
            case 0:
                int direction = UnityEngine.Random.Range(0, 2) == 0 ? -1 : 1;
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetAttributeValue("speed") * direction, 0);
                break;
            case 1:
                //Debug.Log(string.Format("Is performing sequence? {0}", isPerformingSequence));
                if(GetComponent<Rigidbody2D>().velocity.y == 0)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetAttributeValue("speed") * -1);
                }
                while (isPerformingSequence)
                {
                    yield return new WaitForSeconds(1);
                    GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y * -1);
                    Debug.Log(GetComponent<Rigidbody2D>().velocity.y);
                }
                break;
        }
    }

    public IEnumerator MoveToStartPosition()
    {
        while (transform.position.y > 5)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetAttributeValue("speed") * -1);
            if (transform.position.y <= 5)
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                break;
            }
            yield return null;
        }
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        atStartPosition = true;
        yield return new WaitForSeconds(2);
        StartCoroutine(StartShooting());
        StartCoroutine(DoMovement());
    }

    public override IEnumerator StartShooting()
    {
        while (true)
        {
            yield return new WaitForSeconds(GetAttributeValue("shootDelay"));
            EnemyBullet newBullet = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y - 2, transform.position.z), quaternion.identity);
            newBullet.SetBulletStats(GetAttributeValue("bulletSpeed"), GetAttributeValue("damageModifier"), GetAttributeValue("bulletSizeModifier"));
            newBullet.StartMovement(Vector2.down);
        }
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
