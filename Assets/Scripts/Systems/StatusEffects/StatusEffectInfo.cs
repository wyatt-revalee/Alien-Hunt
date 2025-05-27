using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StatusEffectInfo
{
    public string effectName;
    public float duration;
    public float periodicTime;
    public Sprite icon;
    public string description;
    public string ID;
    public bool isDebuff;

    public StatusEffectInfo(string name, float dur, float perTime, string desc, string newID, bool newIsDebuff, Sprite newIcon)
    {
        effectName = name;
        duration = dur;
        periodicTime = perTime;
        description = desc;
        ID = newID;
        icon = newIcon;
    }

    public StatusEffectInfo(string name, float dur, float perTime, string desc, bool newIsDebuff, string newID)
    {
        effectName = name;
        duration = dur;
        periodicTime = perTime;
        description = desc;
        ID = newID;
    }
}
