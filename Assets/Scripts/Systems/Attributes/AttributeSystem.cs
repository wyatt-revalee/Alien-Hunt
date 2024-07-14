using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class AttributeSystem : MonoBehaviour
{

    public Action<Attribute> attributeValueChanged;

    public Dictionary<string, Attribute> attributes = new Dictionary<string, Attribute>
        {
            {"health", new Attribute("health", 10, 99, 1.0f, 0)},
            {"speed", new Attribute("speed", 5, 99, 1.0f, 0)},
            {"defense", new Attribute("defense", 10, 99, 1.0f, 0)}
        };

    void Start()
    {
        StartCoroutine(PrintAttribute("speed"));
    }
    
    private IEnumerator PrintAttribute(string attributeName)
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            Debug.Log(attributes[attributeName].aName + attributes[attributeName].baseValue + attributes[attributeName].multiplier);
        }
    }

    public Attribute GetAttributeFromTag(string attributeName)
    {
        return attributes[attributeName];
    }

    public void ChangeAttributeValue(string attributeToChange, float valueChange, string typeToChange)
    {
        switch (typeToChange)
        {
            case "baseValue":
                attributes[attributeToChange].baseValue += valueChange;
                break;
            case "delta":
                attributes[attributeToChange].delta += valueChange;
                break;
            case "multiplier":
                attributes[attributeToChange].multiplier += valueChange;
                break;
            case "max":
                attributes[attributeToChange].max += valueChange;
                break;
            default:
                break;
        }

        attributeValueChanged?.Invoke(attributes[attributeToChange]);
    }
}
