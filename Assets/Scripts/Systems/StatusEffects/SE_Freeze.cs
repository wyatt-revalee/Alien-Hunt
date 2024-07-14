using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE_Freeze : StatusEffect
{
    public List<string> AddingTags = new List<string>
    {
        "frozen"
    };

    public List<string> PreventingTags = new List<string>
    {
        "frozen"
    };

    new public List<AttributeChange> AttributeEffects = new List<AttributeChange>
    {
        new AttributeChange("speed", "multiplier", -0.5f)
    };
}
