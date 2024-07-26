using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class AttributeSystem : MonoBehaviour
{

    public Action<Attribute> attributeValueChanged;

    public Dictionary<string, Attribute> attributes;

    public void StartAttributePrint(string attributeName)
    {
        StartCoroutine(PrintAttribute(attributeName));
    }
    
    private IEnumerator PrintAttribute(string attributeName)
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            Debug.Log(string.Format(" Name : {0}\n Base Value : {1}\n Multiplier {2}",attributes[attributeName].aName, attributes[attributeName].baseValue, attributes[attributeName].multiplier));
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
