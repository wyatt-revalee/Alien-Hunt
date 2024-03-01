using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerController : MonoBehaviour
{
    //This scripts exists to hold the weapon data and swap it out easily
    public GameObject currentWeapon;
    public GameObject weaponInstance;
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject inventoryMenu;
    public Player player;
    public Rigidbody2D rb;
    public Weapon currentWeaponScript;
    public bool isPaused;

    public event Action OnNewWeaponSet;
    public event Action OnWeaponFired;
    public event Action OnWeaponReloaded;

    void Start()
    {
        CreateWeapon();
        Cursor.visible = false;
    }

    public void OnMove(InputValue value)
    {
        if (!isPaused)
        {
            Debug.Log(value.Get<Vector2>());
            rb.velocity = value.Get<Vector2>() * player.speed.value;
        }
    }

    public void OnShoot()
    {
        if (isPaused)
        {
            return;
        }
        // If there are bullets in the magazine, shoot. If not, reload.
        if (currentWeaponScript.bulletsInMagazine > 0)
        {
            if (currentWeaponScript.isAutomatic)
            {
                // If the weapon is automatic, start a coroutine that repeatedly calls Shoot
                StartCoroutine(AutomaticFire());
            }
            else
            {
                // If the weapon is not automatic, just call Shoot once
                currentWeaponScript.Shoot();
                OnWeaponFired?.Invoke();
            }

            if (currentWeaponScript.bulletsInMagazine == 0)
            {
                currentWeaponScript.Reload();
                StartCoroutine(DoReload());
            }
        }
        else
        {
            if (currentWeaponScript.isReloading)
            {
                return;
            }
            currentWeaponScript.Reload();
            StartCoroutine(DoReload());
        }
    }

    private IEnumerator AutomaticFire()
    {
        while (GetComponent<PlayerInput>().actions["Shoot"].IsPressed())
        {
            if (currentWeaponScript.bulletsInMagazine > 0)
            {
                currentWeaponScript.Shoot();
                OnWeaponFired?.Invoke();
                yield return new WaitForSeconds(1f / (currentWeaponScript.fireRate * player.fireRateModifier.value));
            }
            else
            {
                currentWeaponScript.Reload();
                StartCoroutine(DoReload());
                break;
            }
        }
    }

    public void OnReload()
    {
        currentWeaponScript.bulletsInMagazine = 0;
        currentWeaponScript.Reload();
        StartCoroutine(DoReload());
    }

    IEnumerator DoReload()
    {
        yield return new WaitForSeconds(currentWeaponScript.reloadSpeed * player.reloadSpeedModifier.value);
        OnWeaponReloaded?.Invoke();
    }

    public void SetNewWeapon(GameObject newWeapon)
    {
        currentWeapon = newWeapon;
        CreateWeapon();
    }

    private void CreateWeapon()
    {
        if (weaponInstance != null)
        {
            Destroy(weaponInstance);
        }
        weaponInstance = Instantiate(currentWeapon, transform.position, Quaternion.identity);
        weaponInstance.transform.parent = gameObject.transform;
        currentWeaponScript = weaponInstance.GetComponent<Weapon>();
        currentWeaponScript.player = player;
        OnNewWeaponSet?.Invoke();

        if (isPaused)
        {
            Cursor.visible = true;
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void OnPause()
    {
        PauseGame();
    }

    public void PauseGame()
    {
        if (settingsMenu.activeSelf)
        {
            settingsMenu.SetActive(false);
            pauseMenu.SetActive(true);
        }
        else
        {
            isPaused = !isPaused;
            pauseMenu.SetActive(isPaused);
            transform.GetChild(0).gameObject.SetActive(!isPaused);
            Cursor.visible = isPaused;

            Time.timeScale = isPaused ? 0 : 1;
        }
    }

    public void SetShopping(bool isShopping)
    {
        isPaused = isShopping;
        Cursor.visible = isShopping;
        transform.GetChild(0).gameObject.SetActive(!isShopping);
        currentWeaponScript.isReloading = isShopping;
    }

    public void OnInventory()
    {
        if (isPaused && !inventoryMenu.activeSelf)
        {
            return;
        }
        inventoryMenu.SetActive(!inventoryMenu.activeSelf);
        PauseGame();
        pauseMenu.SetActive(false);
    }

}
