using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.TextCore.Text;



// Potentiall some more movement stats later (ex: max speeds, slow down speeds, etc.)
public class PlayerStats : MonoBehaviour
{

    // Movement
    public CharacterStat movementSpeed = new CharacterStat(1);

    // Health
    public CharacterStat maxHealth = new CharacterStat(10);

    // Combat
    public CharacterStat damage = new CharacterStat(1);


}
