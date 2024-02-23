using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class WeaponController : MonoBehaviour
{
    //This scripts exists to hold the weapon data and swap it out easily
    public GameObject currentWeapon;
    public Weapon currentWeaponScript;
    public GameObject weaponInstance;

    public event Action OnNewWeaponSet;
    public event Action<int> OnWeaponFired;
    public event Action OnWeaponReloaded;

    void Start()
    {
        CreateWeapon();
        Cursor.visible = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Cursor.visible)
        {
            Cursor.visible = false;
        }
    }

    public void OnMove(InputValue value)
    {
        currentWeaponScript.Move(value);
    }

    public void OnShoot()
    {
        //If there are bullets in the magazine, shoot. If not, reload.
        if(currentWeaponScript.bulletsInMagazine > 0)
        {
            currentWeaponScript.Shoot();
            OnWeaponFired?.Invoke(currentWeaponScript.bulletsPerShot);

            if(currentWeaponScript.bulletsInMagazine == 0)
            {
                currentWeaponScript.Reload();
                StartCoroutine(DoReload());
            }
        }
        else
        {
            if(currentWeaponScript.isReloading)
            {
                return;
            }
            currentWeaponScript.Reload();
            StartCoroutine(DoReload());
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
        yield return new WaitForSeconds(currentWeaponScript.reloadSpeed);
        OnWeaponReloaded?.Invoke();
    }

    public void SetNewWeapon(GameObject newWeapon)
    {
        currentWeapon = newWeapon;
        CreateWeapon();
    }

    private void CreateWeapon()
    {
        if(weaponInstance != null)
        {
            Destroy(weaponInstance);
        }
        weaponInstance = Instantiate(currentWeapon, transform.position, Quaternion.identity);
        weaponInstance.transform.parent = gameObject.transform;
        currentWeaponScript = weaponInstance.GetComponent<Weapon>();
        OnNewWeaponSet?.Invoke();
    }

}
