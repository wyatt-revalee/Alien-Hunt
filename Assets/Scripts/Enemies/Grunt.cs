using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class Grunt : Enemy
{

    public override void Awake()
    {
        attributeSystem = GetComponent<AttributeSystem>();
        attributeSystem.attributes = new Dictionary<string, Attribute>
        {
            {"health", new Attribute("health", 1, 99, 1.0f, 0)},
            {"speed", new Attribute("speed", 10, 99, 1.0f, 0)},
            {"defense", new Attribute("defense", 0, 99, 1.0f, 0)},
            {"pointValue", new Attribute("pointValue", 10, 1000, 1.0f, 0)},
            {"shootDelay", new Attribute("shootDelay", 1, 10, 1.0f, 0)},
            {"bulletSpeed", new Attribute("bulletSpeed", 10, 99, 1.0f, 0)},
            {"damageModifier", new Attribute("damageModifier", 1, 99, 1.0f, 0)},
            {"bulletSizeModifier", new Attribute("bulletSizeModifier", 1, 99, 1.0f, 0)},
        };
    }
}
