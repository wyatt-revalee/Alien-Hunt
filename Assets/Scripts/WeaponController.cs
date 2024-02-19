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

    void Start()
    {
        GameObject weaponInstance = Instantiate(currentWeapon, transform.position, Quaternion.identity);
        currentWeaponScript = weaponInstance.GetComponent<Weapon>();
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

}
