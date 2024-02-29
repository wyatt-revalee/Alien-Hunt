using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : Enemy
{
    private Rigidbody2D rb;
    private float timeToChangeDirection = 1f;
    private float circleRadius = 10f;
    private float circleSpeed = 2f * Mathf.PI; // One full circle per second
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
            // Move forward for a second
            rb.velocity = new Vector2(speed * horizontalDirection, 0);
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
