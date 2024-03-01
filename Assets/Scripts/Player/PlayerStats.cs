using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CharacterStats", menuName = "Stats/CharacterStats")]

// Scriptable object to store characters' base stats
public class CharacterStats : ScriptableObject
{
    public Stat maxHealth;
    public Stat movementSpeed;
    public Stat bulletSpeedModifier;
    public Stat bulletSizeModifer;
    public Stat fireRateModifier;
    public Stat reloadSpeedModifier;
    public Stat magazineSizeModifier;
    public Stat damageModifierPercentage;
    public Stat damageModiferFlat;
}