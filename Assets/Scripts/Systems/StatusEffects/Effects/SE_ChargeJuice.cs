using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SE_ChargeJuice : StatusEffect
{
    public override void InitializeEffects()
    {
        attributeEffects.Add(new AttributeChange("fireRate", "multiplier", 0.1f));
        statusEffectInfo = new StatusEffectInfo("Charge Juice", 0f, 0f, "Increases fire rate by 10%", "SE_ChargeJuice", false, icon);
    }

}
