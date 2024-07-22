using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
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
    bool onCooldown;

    public void UseEquipment()
    {
        if(onCooldown)
        {
            return;
        }
        GameObject i_effect = Instantiate(statusEffect, player.transform);
        StatusEffect effectScript = i_effect.GetComponent<StatusEffect>();
        effectScript.owner = player.gameObject;
        effectScript.InitializeEffects();
        effectScript.AttemptApplication();
    }

    

}

