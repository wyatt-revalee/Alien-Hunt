using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SE_SpeedBoost : StatusEffect
{
    public override void InitializeEffects()
    {
        attributeEffects.Add(new AttributeChange("speed", "multiplier", 0.2f));
        statusEffectInfo = new StatusEffectInfo("Speed Boost", 0f, 0f, "Increases speed by 50%.", "SE_SpeedBoost", false, icon);
    }

}
