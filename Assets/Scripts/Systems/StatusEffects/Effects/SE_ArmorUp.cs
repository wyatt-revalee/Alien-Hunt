using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SE_ArmorUp : StatusEffect
{
    public override void InitializeEffects()
    {
        attributeEffects.Add(new AttributeChange("defense", "baseValue", 1f));
        statusEffectInfo = new StatusEffectInfo("Armor Up", 0f, 0f, "Increases armor by 1", "SE_ArmorUp", false, icon);
    }

}
