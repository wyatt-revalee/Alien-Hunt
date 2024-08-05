using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class SE_BubbleShield : StatusEffect
{
    private float baseCooldown = 10;
    public override void InitializeEffects()
    {
        statusEffectInfo = new StatusEffectInfo("Bubble Shield", 0f, 0f, "Shield that blocks 1 hit of damage. Regenerates.", "SE_BubbleShield", false, icon);
    }

    public override void AttemptApplication()
    {
        bool effectFound = false;
        foreach(StatusEffect effect in owner.GetComponent<StatusEffectSystem>().ActiveStatusEffects)
        {
            if(effect.statusEffectInfo.ID == "SE_BubbleShield")
            {
                effect.GetComponent<SE_BubbleShield>().stacks++;
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
            Destroy(collider2D.gameObject);
            StartCoroutine(ShieldCooldown());
        }
    }

    public IEnumerator ShieldCooldown()
    {
        ShieldActivation(false);
        float cooldown = (float)(baseCooldown * (Math.Pow(0.9f, stacks)));
        yield return new WaitForSeconds(cooldown);
        ShieldActivation(true);
    }

    public void ShieldActivation(bool activate)
    {
        GetComponent<CircleCollider2D>().enabled = activate;
        GetComponent<SpriteRenderer>().enabled = activate;
    }
}
