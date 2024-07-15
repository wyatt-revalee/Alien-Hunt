using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SE_RapidFire : StatusEffect
{
    public override void InitializeEffects()
    {
        attributeEffects.Add(new AttributeChange("speed", "multiplier", 0.5f));
        statusEffectInfo = new StatusEffectInfo("Speed Boost", 5f, 1f, "Increases speed by 50%.", "SE_SpeedBoost", icon);
    }

    override public IEnumerator PeriodicEffectApplication(float seconds)
    {
        while (applyPeriodicEffects)
        {
            Debug.Log("Effect!");
            yield return new WaitForSeconds(seconds);
        }
    }

}
