using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SE_SpeedBoost : StatusEffect
{
    void Start()
    {
        attributeEffects.Add(new AttributeChange("speed", "multiplier", 0.5f));
        statusEffectInfo = new StatusEffectInfo("Speed Boost", 5f, 0f, "Increases speed by 50%.", "SE_SpeedBoost", icon);
    }

}
