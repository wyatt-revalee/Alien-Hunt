using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class Attribute
{
    public string aName = "Attribute";
    public int baseValue = 10;
    public int max = 99;
    public float multiplier = 1.0f;
    public int delta = 0;
    public Image icon;
    
    public Attribute(string newName, int newBase, int newMax, float newMult, int newDelta){
        aName = newName;
        baseValue = newBase;
        max = newMax;
        multiplier = newMult;
        delta = newDelta;
    }

    public int GetTrueValue()
    {
        return (int)(baseValue * multiplier) + delta;
    }
}
