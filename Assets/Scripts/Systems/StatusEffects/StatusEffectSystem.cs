using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectSystem : MonoBehaviour
{

    public List<StatusEffect> ActiveStatusEffects;
    public event Action<StatusEffect> StatusEffectWasRemoved;
    public event Action<StatusEffect> StatusEffectWasAdded;

    public List<StatusEffect> GetStatusEffects()
    {
        return ActiveStatusEffects;
    } 

    public void AddStatusEffect(StatusEffect effectToAdd)
    {
        //Debug.Log("adding");
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

    public void CallStatusEffectAdded(StatusEffect effectAdded)
    {
        StatusEffectWasAdded?.Invoke(effectAdded);
    }
}
