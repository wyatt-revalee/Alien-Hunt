using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SE_BuffLength : StatusEffect
{
    public override void InitializeEffects()
    {
        attributeEffects.Add(new AttributeChange("buffTime", "baseValue", 1));
        statusEffectInfo = new StatusEffectInfo("Buff length increase", 0f, 0f, "Extends buff duration by 1 second per stack.", "SE_BuffLength", false, icon);
    }

}
