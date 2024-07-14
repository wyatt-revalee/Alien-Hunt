using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerController : MonoBehaviour
{

    public event Action<Vector2> OnMovement;
    public bool isPaused;
    public Player player;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMove(InputValue value)
    {
        OnMovement?.Invoke(value.Get<Vector2>());
        if (!isPaused)
        {
            rb.velocity = value.Get<Vector2>() * (player.movementSpeed * player.GetComponent<AttributeSystem>().attributes["speed"].GetTrueValue());
        }
    }
}
