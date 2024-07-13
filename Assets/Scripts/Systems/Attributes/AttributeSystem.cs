using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class AttributeSystem : MonoBehaviour
{

    public Dictionary<string, Attribute> Attributes = new Dictionary<string, Attribute>
        {
            {"health", new Attribute("health", 10, 99, 1.0f, 0)},
            {"speed", new Attribute("speed", 10, 99, 1.0f, 0)},
            {"defense", new Attribute("defense", 10, 99, 1.0f, 0)}
        };

    public Attribute GetAttributeFromTag(string attributeName)
    {
        return Attributes[attributeName];
    }

    public void ChangeAttributeValue(Attribute attributeToChange, int valueChange, string typeToChange)
    {
        switch (typeToChange)
        {
            case "baseValue":
                Attributes[attributeToChange.aName].baseValue += valueChange;
                break;
            case "delta":
                Attributes[attributeToChange.aName].delta += valueChange;
                break;
            case "multiplier":
                Attributes[attributeToChange.aName].multiplier += valueChange;
                break;
            case "max":
                Attributes[attributeToChange.aName].max += valueChange;
                break;
            default:
                break;
        }

    }
}
