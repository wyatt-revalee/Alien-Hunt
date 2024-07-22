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
    public Action<int> onStartCooldown;
    public Action<int> onUseEquipment;
    public Sprite icon;
    bool onCooldown;

    public void UseEquipment()
    {
        if(onCooldown)
        {
            return;
        }

        GameObject i_effect = Instantiate(statusEffect, transform);
        StatusEffect effectScript = i_effect.GetComponent<StatusEffect>();
        effectScript.owner = player.gameObject;
        effectScript.InitializeEffects();
        effectScript.AttemptApplication();
        StartCoroutine(StartUseBuffer());
    }

    public IEnumerator StartUseBuffer()
    {
        onUseEquipment?.Invoke(cooldownBuffer);
        yield return new WaitForSeconds(cooldownBuffer);
        StartCoroutine(DoCooldown());
    }

    public IEnumerator DoCooldown()
    {
        onStartCooldown?.Invoke(cooldown);
        onCooldown = true;
        yield return new WaitForSeconds(cooldown);
        onCooldown = false;
    }

}

