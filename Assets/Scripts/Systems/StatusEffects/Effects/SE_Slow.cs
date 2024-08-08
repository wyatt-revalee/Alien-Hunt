using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SE_Slow : StatusEffect
{
    public override void InitializeEffects()
    {
        attributeEffects.Add(new AttributeChange("speed", "multiplier", -0.05f * stacks));
        statusEffectInfo = new StatusEffectInfo("Slow", 0f, 0f, "Decreases speed by 50%.", "SE_Slow", false, icon);
    }
}
