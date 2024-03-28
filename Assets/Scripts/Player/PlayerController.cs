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
    public GameObject weaponSelector;
    public Player player;
    public Rigidbody2D rb;
    public Weapon currentWeaponScript;
    public bool isPaused;
    public PlayerInput playerInput;

    public List<StoredWeaponInfo> weapons;

    public event Action OnNewWeaponSet;
    public event Action OnWeaponsUpdated;
    public event Action OnWeaponFired;
    public event Action OnWeaponReloaded;
    public event Action OnPrimaryButtonClick;
    public event Action<Vector2> OnMovement;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        AddWeapon(currentWeapon);
        Cursor.visible = false;
    }

    public void OnMove(InputValue value)
    {
        OnMovement?.Invoke(value.Get<Vector2>());
        if (!isPaused)
        {
            rb.velocity = value.Get<Vector2>() * (player.movementSpeed * player.movementSpeedModifer.value);
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

    public void OnSwapWeapon()
    {
        if(weapons.Count > 0)
        {
            isPaused = true;
            weaponSelector.SetActive(true);
            weaponSelector.GetComponent<WeaponSelector>().StartWeaponSelection();
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

    public void AddWeapon(GameObject newWeapon)
    {
        StoredWeaponInfo newWeaponInfo = new StoredWeaponInfo();
        newWeaponInfo.name = newWeapon.name;
        newWeaponInfo.weaponObject = newWeapon;
        newWeaponInfo.currentAmmo = newWeapon.GetComponent<Weapon>().ammoCapacity; ;
        newWeaponInfo.bulletsInMagazine = newWeapon.GetComponent<Weapon>().magazineSize; ;
        weapons.Add(newWeaponInfo);
        SetNewWeapon(newWeapon);
        OnWeaponsUpdated?.Invoke();
    }

    public void SetNewWeapon(GameObject newWeapon)
    {
        if(currentWeaponScript != null)
        {
            //Debug.Log("Caching weapon info");
            CacheWeaponInfo();
        }
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
        StoredWeaponInfo cachedWeaponInfo = FetchWeaponInfo(currentWeaponScript.weaponID);
        if(cachedWeaponInfo != null)
        {
            //Debug.Log(cachedWeaponInfo.bulletsInMagazine);
            currentWeaponScript.bulletsInMagazine = cachedWeaponInfo.bulletsInMagazine;
            currentWeaponScript.currentAmmo = cachedWeaponInfo.currentAmmo;
        }
        currentWeaponScript.player = player;
        weaponInstance.GetComponent<SpriteRenderer>().enabled = false;
        OnNewWeaponSet?.Invoke();

        if (isPaused)
        {
            Cursor.visible = true;
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void CacheWeaponInfo()
    {
        for(int weaponSlot = 0; weaponSlot < weapons.Count; weaponSlot++)
        {
            if(weapons[weaponSlot].weaponObject.GetComponent<Weapon>().weaponID == currentWeaponScript.weaponID)
            {
                weapons[weaponSlot].weaponObject = currentWeapon;
                weapons[weaponSlot].currentAmmo = currentWeaponScript.currentAmmo;
                weapons[weaponSlot].bulletsInMagazine = currentWeaponScript.bulletsInMagazine;

            }
        }
    }

    private StoredWeaponInfo FetchWeaponInfo(int id)
    {
        foreach(StoredWeaponInfo info in weapons)
        {
            if(info.weaponObject.GetComponent<Weapon>().weaponID == id)
            {
                //Debug.Log("Cached weapon info found");
                return info;
            }
        }
        //Debug.Log("Weapon info does not exist");
        return null;

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
            SetControlMap();
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
        SetControlMap();
        currentWeaponScript.isReloading = isShopping;
        rb.velocity = Vector2.zero;
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

    public void OnSelect()
    {
        OnPrimaryButtonClick?.Invoke();
    }

    public void OnMoveSelection(InputValue value)
    {
        OnMovement?.Invoke(value.Get<Vector2>());
    }

    public void SetControlMap()
    {
        string newMap = isPaused ? "Menu" : "Main";
        GetComponent<PlayerInput>().SwitchCurrentActionMap(newMap);
    }

}

[System.Serializable]
public class StoredWeaponInfo
{
    public string name;
    public GameObject weaponObject;
    public int currentAmmo;
    public int bulletsInMagazine;
}