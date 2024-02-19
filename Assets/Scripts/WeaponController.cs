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
        currentWeaponScript.Shoot();
        
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
