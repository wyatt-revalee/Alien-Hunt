using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeChange
{

    public string attributeName;
    public string changeType;
    public float changeAmount;

    public AttributeChange(string name, string type, float amount)
    {
        attributeName = name;
        changeType = type;
        changeAmount = amount;
    }
}
