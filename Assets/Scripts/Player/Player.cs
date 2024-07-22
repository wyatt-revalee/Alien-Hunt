using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public event Action<Vector2> OnMovement;
    public event Action<Attribute> OnHealthChanged;
    public event Action<GameObject> OnEquipmentAdded;
    public event Action<GameObject> OnEquipmentRemoved;
    public event Action OnWeaponFired;
    public event Action<bool> OnShotHit;
    public event Action<int> OnEnemyKilled;
    public Inventory inventory;
    public bool isPaused;
    public int coins;
    public Rigidbody2D rb;
    public Bullet bullet;
    public AttributeSystem attributeSystem;
    public GameObject activeEquipment;

    void Awake()
    {
        attributeSystem = GetComponent<AttributeSystem>();
        attributeSystem.attributes = new Dictionary<string, Attribute>
        {
            {"health", new Attribute("health", 10, 10, 1.0f, 0)},
            {"speed", new Attribute("speed", 1, 99, 1.0f, 0)},
            {"defense", new Attribute("defense", 10, 99, 1.0f, 0)},
            {"bulletSpeed", new Attribute("bulletSpeed", 10, 99, 1.0f, 0)},
            {"damageModifier", new Attribute("damageModifier", 1, 99, 1.0f, 0)},
            {"bulletSizeModifier", new Attribute("bulletSizeModifier", 1, 99, 1.0f, 0)},
            {"fireRate", new Attribute("fireRate", 1, 99, 1.0f, 0)}
        };
        OnHealthChanged?.Invoke(attributeSystem.attributes["health"]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetAttributeValue(string attribute)
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

    private void OnInventory()
    {
        inventory.gameObject.SetActive(!inventory.gameObject.activeSelf);
    }

    private void OnActiveEquipment()
    {
        activeEquipment.GetComponent<ActiveEquipment>().UseEquipment();
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

    public void Damage(int amount)
    {
        float currentHealth = GetComponent<AttributeSystem>().attributes["health"].baseValue -= amount;
        OnHealthChanged?.Invoke(attributeSystem.attributes["health"]);

        if(currentHealth <= 0)
        {
            SceneManager.LoadScene(0);
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

    public void AddEquipment(GameObject equipmentItem)
    {
        GameObject equipment = Instantiate(equipmentItem.GetComponent<Item>().activeEquipment, transform);
        equipment.GetComponent<ActiveEquipment>().player = this;
        OnEquipmentAdded?.Invoke(equipment);

        if(activeEquipment != null)
        {
            RemoveEquipment(equipmentItem);
        }
        activeEquipment = equipment;
    }

    public void RemoveEquipment(GameObject equipmentItem)
    {
        OnEquipmentRemoved?.Invoke(equipmentItem.GetComponent<Item>().activeEquipment);
        inventory.RemoveItemFromInventory(equipmentItem);
        Destroy(activeEquipment.gameObject);
    }

}
