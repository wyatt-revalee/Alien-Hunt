using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Runtime.InteropServices;

public class Player : MonoBehaviour
{

    public event Action<Vector2> OnMovement;
    public event Action OnWeaponFired;
    public event Action<bool> OnShotHit;
    public event Action<int> OnEnemyKilled;
    public bool isPaused;
    public int coins;
    public Rigidbody2D rb;
    public Bullet bullet;

    void Awake()
    {
        GetComponent<AttributeSystem>().attributes = new Dictionary<string, Attribute>
        {
            {"health", new Attribute("health", 10, 99, 1.0f, 0)},
            {"speed", new Attribute("speed", 1, 99, 1.0f, 0)},
            {"defense", new Attribute("defense", 10, 99, 1.0f, 0)},
            {"bulletSpeed", new Attribute("bulletSpeed", 10, 99, 1.0f, 0)},
            {"damageModifier", new Attribute("damageModifier", 1, 99, 1.0f, 0)},
            {"bulletSizeModifier", new Attribute("bulletSizeModifier", 1, 99, 1.0f, 0)},
            {"fireRate", new Attribute("fireRate", 1, 99, 1.0f, 0)}
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private float GetAttributeValue(string attribute)
    {
        return GetComponent<AttributeSystem>().attributes[attribute].GetTrueValue();
    }

    public void OnMove(InputValue value)
    {
        OnMovement?.Invoke(value.Get<Vector2>());
        if (!isPaused)
        {
            rb.velocity = value.Get<Vector2>() * (3 * GetComponent<AttributeSystem>().attributes["speed"].GetTrueValue());
        }
    }



    private void OnShoot()
    {
        if (GetAttributeValue("fireRate") < 1)
        {
            // If the weapon is automatic, start a coroutine that repeatedly calls Shoot
            StartCoroutine(AutomaticFire());
        }
        else
        {
            // If the weapon is not automatic, just call Shoot once
            ShootBullet();
        }
    }

    private void ShootBullet()
    {
        Bullet newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        newBullet.owner = gameObject;
        newBullet.SetBulletStats(GetAttributeValue("bulletSpeed"), GetAttributeValue("damageModifier"), GetAttributeValue("bulletSizeModifier"));
        newBullet.StartMovement();
        OnWeaponFired?.Invoke();
    }

    private IEnumerator AutomaticFire()
    {

        Debug.Log("Automatic fire started");
        while (GetComponent<PlayerInput>().actions["Shoot"].IsPressed())
        {
            if (GetAttributeValue("fireRate") < 1)
            {
                ShootBullet();
                yield return new WaitForSeconds(GetAttributeValue("fireRate"));
            }
            else
            {
                break;
            }
        }
    }

    public void CallShotHit(bool hit, bool enemyKilled = false, int pointValue = 0)
    {
        OnShotHit?.Invoke(hit);

        if(enemyKilled)
        {
            OnEnemyKilled?.Invoke(pointValue);
        }
    }

    public void AddCoins(int amount)
    {
        coins += amount;
    }

    public void RemoveCoins(int amount)
    {
        coins -= amount;
    }

}
