using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectSystem : MonoBehaviour
{

    List<StatusEffect> ActiveStatusEffects;

    public List<StatusEffect> GetStatusEffects()
    {
        return ActiveStatusEffects;
    } 

    public void AddStatusEffect(StatusEffect effectToAdd)
    {
        effectToAdd.AttemptApplication();
    }

    public void RemoveStatusEffect(StatusEffect effectToRemove)
    {
        if(!ActiveStatusEffects.Contains(effectToRemove))
        {
            Debug.Log("No status effect to remove.");
            return;
        }

        ActiveStatusEffects.Remove(effectToRemove);
        //remove tags from player
        effectToRemove.ApplyAttributeEffects(false);
        effectToRemove.ClearTimers();
        //call effect removed
    }

    public void PrintAllEffects()
    {
        foreach(StatusEffect effect in ActiveStatusEffects)
        {
            print(effect.name);
        }
    }

}
