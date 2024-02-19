using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evader : Enemy
{
    void Start()
    {
        StartCoroutine(MoveUpAndDown());
    }

    public void FixedUpdate()
    {
        if (isMoving)
        {
            MoveAcrossScreen();
        }
    }

    public void MoveAcrossScreen()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed * horizontalDirection, speed * verticalDirection);
    }

    public IEnumerator MoveUpAndDown()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            verticalDirection *= -1;
        }
    }
}
