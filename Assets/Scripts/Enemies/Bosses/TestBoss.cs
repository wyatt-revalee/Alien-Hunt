using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TestBoss : Enemy
{

    public bool isPerformingSequence;
    public string currentSequence;
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
            {"bulletSpeed", new Attribute("bulletSpeed", 10, 99, 1.0f, 0)},
            {"damageModifier", new Attribute("damageModifier", 1, 99, 1.0f, 0)},
            {"bulletSizeModifier", new Attribute("bulletSizeModifier", 1, 99, 1.0f, 0)},
        };
    }

    public override void StartMovement(int horizontal = 0, int vertical = 0) // Moves boss to area where it will begin its attacking
    {
        if(isPerformingSequence)
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
            currentSequence = sequences[Random.Range(0, sequences.Count)];
            isPerformingSequence = true;
            StartCoroutine(PerformSequence(sequences.IndexOf(currentSequence)));
            Debug.Log(string.Format("Performing {0} sequence", currentSequence));
            yield return new WaitForSeconds(10);
            isPerformingSequence = false;
            StopCoroutine(PerformSequence(sequences.IndexOf(currentSequence)));
        }

    }

    IEnumerator PerformSequence(int sequenceNumber)
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //Debug.Log(sequenceNumber);
        switch (sequenceNumber)
        {
            case 0:
                int direction = Random.Range(0, 2) == 0 ? -1 : 1;
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetAttributeValue("speed") * direction, 0);
                break;
            case 1:
                Debug.Log(string.Format("Is performing sequence? {0}", isPerformingSequence));
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetAttributeValue("speed") * -1);
                while (isPerformingSequence)
                {
                    Debug.Log("Moving updown");
                    yield return new WaitForSeconds(1);
                    GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y * -1);
                    yield return new WaitForSeconds(1);
                    GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y * -1);
                }
                break;
        }
    }

    public IEnumerator MoveToStartPosition()
    {
        while (transform.position.y > 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetAttributeValue("speed") * -1);
            if (transform.position.y <= 0)
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                break;
            }
            yield return null;
        }
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        StartCoroutine(StartShooting());
        StartCoroutine(DoMovement());
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
