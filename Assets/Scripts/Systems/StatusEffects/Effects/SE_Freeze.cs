using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SE_Freeze : StatusEffect
{
    void Start()
    {
        attributeEffects.Add(new AttributeChange("speed", "multiplier", -0.5f));
        statusEffectInfo = new StatusEffectInfo("freeze", 5f, 0f, "Halves speed.", "SE_Freeze", icon);
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        collider2D.transform.GetComponent<StatusEffectSystem>().AddStatusEffect(this);
        return;
    }
}
