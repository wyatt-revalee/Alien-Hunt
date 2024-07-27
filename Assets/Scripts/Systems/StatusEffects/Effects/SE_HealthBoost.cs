using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE_HealthBoost : StatusEffect
{
    public override void InitializeEffects()
    {
        attributeEffects.Add(new AttributeChange("health", "baseValue", 10));
        attributeEffects.Add(new AttributeChange("health", "max", 10));
        statusEffectInfo = new StatusEffectInfo("Health Boost", 0, 0, "Boosts Health by 10", "SE_HealthBoost", false, icon);
    }

    public override void AttemptApplication()
    {
        if (!CheckTagStartConditions())
        {
            return;
        }
        //Debug.Log("Adding " + this.name);
        owner.GetComponent<StatusEffectSystem>().ActiveStatusEffects.Add(this);
        owner.GetComponent<StatusEffectSystem>().CallStatusEffectAdded(this);
        StartStatusEffect();
        owner.GetComponent<Player>().UpdateHealth();
    }
}
