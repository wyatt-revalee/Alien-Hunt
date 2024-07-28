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
    public event Action<bool> OnGamePaused;
    public Action<int> onStartEquipmentCooldown;
    public Action<int> onUseEquipment;
    public Inventory inventory;
    public PauseMenu pauseMenu;
    public int coins;
    public Rigidbody2D rb;
    public Bullet bullet;
    public AttributeSystem attributeSystem;
    public GameObject activeEquipment;
    public bool equipmentOnCooldown;
    private float lastShotTime;
    private float baseShootTime = 0.25f;

    void Awake()
    {
        attributeSystem = GetComponent<AttributeSystem>();
        attributeSystem.attributes = new Dictionary<string, Attribute>
        {
            {"health", new Attribute("health", 10, 10, 1.0f, 0)},                                       // Player hp
            {"speed", new Attribute("speed", 1, 99, 1.0f, 0)},                                          // Player move speed
            {"defense", new Attribute("defense", 10, 99, 1.0f, 0)},                                     // How much damage is reduced when player is hit, cannot reduce damange below 1
            {"bulletSpeed", new Attribute("bulletSpeed", 10, 99, 1.0f, 0)},                             // How fast bullets travel
            {"damageModifier", new Attribute("damageModifier", 1, 99, 1.0f, 0)},                        // How much damage player does
            {"bulletSizeModifier", new Attribute("bulletSizeModifier", 1, 99, 1.0f, 0)},                // How big player's bullets are
            {"fireRate", new Attribute("fireRate", 1, 99, 1.0f, 0)},                                    // Modifer for how fast player can shoot
            {"equipmentCooldownModifier", new Attribute("equipmentCooldownModifier", 1, 99, 1.0f, 0)},  // Effects how long equipment cooldown is. Lower value = smaller cooldown
            {"buffTime", new Attribute("buffTime", 1, 99, 1.0f, 0)},                                    // Effects how long buffs stay applied
        };
        OnHealthChanged?.Invoke(attributeSystem.attributes["health"]);
        //attributeSystem.StartAttributePrint("health");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetAttributeValue(string attribute)
    {
        return GetComponent<AttributeSystem>().attributes[attribute].GetTrueValue();
    }

    void OnPause()
    {
        pauseMenu.PauseHit();   
    }

    public void OnMove(InputValue value)
    {
        OnMovement?.Invoke(value.Get<Vector2>());
        rb.velocity = value.Get<Vector2>() * (10 * GetComponent<AttributeSystem>().attributes["speed"].GetTrueValue());
    }

    private void OnInventory()
    {
        inventory.gameObject.SetActive(!inventory.gameObject.activeSelf);
    }

    // Uses equipment if it exists and is not on cooldown
    // Then starts the 'use' period, then the cooldown period afterwards
    private void OnActiveEquipment()
    {
        if(activeEquipment == null || equipmentOnCooldown)
        {
            return;
        }
        activeEquipment.GetComponent<ActiveEquipment>().UseEquipment();
        StartCoroutine(StartEquipmentUseBuffer());
        equipmentOnCooldown = true;
    }

    public IEnumerator StartEquipmentUseBuffer()
    {
        int cooldown = (int)(activeEquipment.GetComponent<ActiveEquipment>().cooldown * GetAttributeValue("equipmentCooldownModifier"));
        int cooldownBuffer = (int)(activeEquipment.GetComponent<ActiveEquipment>().cooldownBuffer * GetAttributeValue("equipmentUseTimeModifier"));
        onUseEquipment?.Invoke(cooldownBuffer);
        yield return new WaitForSeconds(cooldownBuffer);
        StartCoroutine(DoEquipmentCooldown(cooldown));
    }

    public IEnumerator DoEquipmentCooldown(int cooldown)
    {
        onStartEquipmentCooldown?.Invoke(cooldown);
        equipmentOnCooldown = true;
        yield return new WaitForSeconds(cooldown);
        equipmentOnCooldown = false;
    }

    private void OnShoot()
    {
        if(Time.time - lastShotTime > (baseShootTime / GetAttributeValue("fireRate")))
        {
            // If the weapon is not automatic, just call Shoot once
            ShootBullet();
            lastShotTime = Time.time;
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
        int damageToTake = Math.Max(amount - (int)GetAttributeValue("defense"), 1);
        Debug.Log(damageToTake);
        float currentHealth = GetComponent<AttributeSystem>().attributes["health"].baseValue -= damageToTake;
        OnHealthChanged?.Invoke(attributeSystem.attributes["health"]);

        if(currentHealth <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void UpdateHealth()
    {
        OnHealthChanged?.Invoke(attributeSystem.attributes["health"]);
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

        if(activeEquipment != null)
        {
            Debug.Log("Removing equipment: " + activeEquipment);
            RemoveEquipment();
        }
        OnEquipmentAdded?.Invoke(equipment);
        activeEquipment = equipment;
    }

    public void RemoveEquipment()
    {
        OnEquipmentRemoved?.Invoke(activeEquipment);
        inventory.RemoveItemFromInventory(activeEquipment.GetComponent<ActiveEquipment>().id);
        Destroy(activeEquipment);
    }

}
