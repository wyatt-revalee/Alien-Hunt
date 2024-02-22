using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Enemy : MonoBehaviour
{

    [Header("Enemy Stats")]
    public int health;
    public int speed;
    public int pointValue;
    public int cost;

    [Header("Enemy Components")]
    public Animator animator;
    public Canvas pointPopup;

    [Header("Enemy Movement")]
    public int horizontalDirection;
    public int verticalDirection;
    public bool isMoving = false;

    public GameObject itemDrop;
    public event Action<bool> OnDeath;

    public void Damage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            GetComponent<Collider2D>().enabled = false;
            isMoving = false;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            StartCoroutine(Death());
        }
        else
        {
            StartCoroutine(DamageFlash());
        }
    }

    public IEnumerator Death()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.5f);

        if(itemDrop != null)
        {
            Instantiate(itemDrop, transform.position, Quaternion.identity);
        }

        var popup = Instantiate(pointPopup, transform.position, Quaternion.identity);
        popup.GetComponent<PointPopup>().SetValue(pointValue);
        OnDeath?.Invoke(true);
        Destroy(gameObject);
    }

    public IEnumerator DamageFlash()
    {
        Color basecolor = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().color = basecolor;
    }

    public void SetMoveDirection(int hDir, int vDir)
    {
        horizontalDirection = hDir;
        verticalDirection = vDir;
        isMoving = true;
    }

    public void HitKillZone()
    {
        OnDeath?.Invoke(false);
        Destroy(gameObject);
    }

}
