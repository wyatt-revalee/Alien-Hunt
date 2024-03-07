using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.TextCore.Text;



// Potentiall some more movement stats later (ex: max speeds, slow down speeds, etc.)
public class PlayerStats : MonoBehaviour
{

    // Movement
    public CharacterStat groundSpeed = new CharacterStat(15);
    public CharacterStat airMoveSpeed = new CharacterStat(10);
    public CharacterStat maxFallSpeed = new CharacterStat(10);
    public CharacterStat jumpHeight = new CharacterStat(300);

    // Health
    public CharacterStat maxHealth = new CharacterStat(10);

    // Mana
    public CharacterStat maxMana = new CharacterStat(100);
    public CharacterStat manaRegen = new CharacterStat(0.2f);

    // Combat
    public CharacterStat knockback = new CharacterStat(1);


}
