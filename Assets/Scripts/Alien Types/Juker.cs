using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Juker : Enemy
{
    private Rigidbody2D rb;
    private bool moveSignalSent = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate()
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
            // Move forward for two seconds second
            rb.velocity = new Vector2(speed * horizontalDirection, 0);
            yield return new WaitForSeconds(2f);

            // Reverse direction for a second
            float startTime = Time.time;
            while (Time.time < startTime + 1f)
            {
                rb.velocity = new Vector2(speed * horizontalDirection * -1, 0);
                yield return null;
            }
        }
    }
}
