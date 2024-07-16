using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SE_RapidFire : StatusEffect
{
    public override void InitializeEffects()
    {
        attributeEffects.Add(new AttributeChange("fireRate", "multiplier", -0.9f));
        statusEffectInfo = new StatusEffectInfo("Rapdi Fire", 5f, 1f, "Allows for automatic fire", "SE_RapidFire", icon);
    }

    override public IEnumerator PeriodicEffectApplication(float seconds)
    {
        while (applyPeriodicEffects)
        {
            //Debug.Log("Effect!");
            yield return new WaitForSeconds(seconds);
        }
    }

}
