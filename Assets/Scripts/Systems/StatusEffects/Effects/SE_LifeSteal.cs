using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SE_LifeSteal : StatusEffect
{
    public override void InitializeEffects()
    {
        statusEffectInfo = new StatusEffectInfo("Life Steal", 0f, 0f, "Heal on enemy hit", "SE_LifeSteal", false, icon);
    }

    public override void StartStatusEffect()
    {
        AddTags();
        owner.GetComponent<Player>().OnShotHit += ActivateLifeSteal;
        owner.GetComponent<Player>().bulletColor = new Color32(255, 0, 0, 255);
    }

    public void ActivateLifeSteal(bool hit)
    {
        if(hit)
        {
            owner.GetComponent<Player>().Heal(1);
        }
    }

}
