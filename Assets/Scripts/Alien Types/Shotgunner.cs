using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgunner : Enemy
{
    public void FixedUpdate()
    {
        if (isMoving)
        {
            MoveAcrossScreen();
        }
    }

    public void MoveAcrossScreen()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed * horizontalDirection, 0);
    }
}
