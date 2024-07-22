using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE_NitroBurst : StatusEffect
{
    public override void InitializeEffects()
    {
        attributeEffects.Add(new AttributeChange("speed", "multiplier", 2f));
        statusEffectInfo = new StatusEffectInfo("Nitro Boost", 10f, 0f, "Increases speed by 50%.", "SE_NitroBurst", false, icon);
    }
}
