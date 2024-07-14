using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectSystem : MonoBehaviour
{

    List<StatusEffect> ActiveStatusEffects;
    public event Action<StatusEffect> StatusEffectWasRemoved;

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
        effectToRemove.RemoveTags();
        effectToRemove.ApplyAttributeEffects(false);
        effectToRemove.ClearTimers();
        StatusEffectWasRemoved?.Invoke(effectToRemove);
    }

    public void PrintAllEffects()
    {
        foreach(StatusEffect effect in ActiveStatusEffects)
        {
            print(effect.name);
        }
    }

}
