using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class SE_DeflectBullet : StatusEffect
{
    public override void InitializeEffects()
    {
        statusEffectInfo = new StatusEffectInfo("Deflect Bullet", 0f, 0f, "Adds a chance to reflect a bullet back at an enemy.", "SE_DeflectBullet", false, icon);
    }

    public override void AttemptApplication()
    {
        bool effectFound = false;
        foreach(StatusEffect effect in owner.GetComponent<StatusEffectSystem>().ActiveStatusEffects)
        {
            if(effect.statusEffectInfo.ID == "SE_DeflectBullet")
            {
                effect.GetComponent<SE_DeflectBullet>().stacks++;
                effectFound = true;
                Destroy(gameObject);
            }
        }
        if(!effectFound)
        {
            base.AttemptApplication();
        }
    }

    void Update()
    {
        transform.position = owner.transform.position;
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.gameObject.layer == 12)
        {
            if(RollReflectChance())
            {
                DeflectBullet(collider2D.gameObject);
            }
        }
    }

    bool RollReflectChance()
    {
        int deflectChance = 10 + 2 * (stacks - 1); // 10 -> 12 -> 14 ...
        int roll = UnityEngine.Random.Range(1, 101);
        return roll <= deflectChance;
    }

    void DeflectBullet(GameObject bullet)
    {
        bullet.GetComponent<Bullet>().StartMovement(Vector2.up);
        bullet.GetComponent<Bullet>().isPlayerBullet = true;
        bullet.GetComponent<Bullet>().owner = owner;
    }

}
