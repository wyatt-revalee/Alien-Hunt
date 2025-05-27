using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Attribute
{
    public string aName = "Attribute";
    public float baseValue = 10;
    public float max = 99;
    public float multiplier = 1;
    public float delta = 0;
    public Image icon;
    
    public Attribute(string newName, int newBase, int newMax, float newMult, int newDelta){
        aName = newName;
        baseValue = newBase;
        max = newMax;
        multiplier = newMult;
        delta = newDelta;
    }

    public float GetTrueValue()
    {
        return (baseValue * multiplier) + delta;
    }
}
