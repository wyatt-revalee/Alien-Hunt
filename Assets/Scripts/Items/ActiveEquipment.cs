using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActiveEquipment : MonoBehaviour
{

    public int cooldown;
    public int cooldownBuffer;
    public GameObject statusEffect;
    public Player player;
    public Sprite icon;
    public string id;
    public bool isBuff;

    public void UseEquipment()
    {
        GameObject i_effect = Instantiate(statusEffect, player.transform);
        StatusEffect effectScript = i_effect.GetComponent<StatusEffect>();
        effectScript.InitializeEffects();
        effectScript.owner = player.gameObject;
        effectScript.AttemptApplication();
    }

    

}

