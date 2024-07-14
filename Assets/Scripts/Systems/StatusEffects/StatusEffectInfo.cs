using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class StatusEffectInfo
{
    public string effectName;
    public float duration;
    public float periodicTime;
    public Sprite icon;
    public string description;
    public string ID;

    public StatusEffectInfo(string name, float dur, float perTime, string desc, string newID, Sprite newIcon)
    {
        effectName = name;
        duration = dur;
        periodicTime = perTime;
        description = desc;
        ID = newID;
        icon = newIcon;
    }

    public StatusEffectInfo(string name, float dur, float perTime, string desc, string newID)
    {
        effectName = name;
        duration = dur;
        periodicTime = perTime;
        description = desc;
        ID = newID;
    }
}
