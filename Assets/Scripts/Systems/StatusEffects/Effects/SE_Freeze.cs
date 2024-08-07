using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SE_Freeze : StatusEffect
{
    public override void InitializeEffects()
    {
        attributeEffects.Add(new AttributeChange("speed", "multiplier", -0.5f));
        statusEffectInfo = new StatusEffectInfo("freeze", 5f, 0f, "Halves speed.", "SE_Freeze", true, icon);
    }

}
