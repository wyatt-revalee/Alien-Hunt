using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;


public abstract class StatusEffect : MonoBehaviour
{
    public GameObject owner;
    public Sprite icon;
    public List<string> requiredTags;
    public List<string> preventingTags;
    public List<string> addingTags;
    public bool applyPeriodicEffects;
    public List<AttributeChange> attributeEffects = new List<AttributeChange>();
    public StatusEffectInfo statusEffectInfo;

    public void AttemptApplication()
    {
        if(!CheckTagStartConditions())
        {
            return;
        }
        //Debug.Log("Adding " + this.name);
        owner.GetComponent<StatusEffectSystem>().ActiveStatusEffects.Add(this);
        owner.GetComponent<StatusEffectSystem>().CallStatusEffectAdded(this);
        StartStatusEffect();
    }

    public virtual void InitializeEffects()
    {

    }

    public bool CheckTagStartConditions()
    {
        if(owner.GetComponent<GameplayTagSystem>().HasAllTags(requiredTags) && !owner.GetComponent<GameplayTagSystem>().HasAnyTags(preventingTags))
        {
            return true;
        }
        return false;
    }

    private void StartStatusEffect()
    {
        AddTags();
        ApplyAttributeEffects(true);
        applyPeriodicEffects = true;
        StartCoroutine(PeriodicEffectApplication(statusEffectInfo.periodicTime));
        if(statusEffectInfo.duration > 0)
        {
            StartCoroutine(EndStatusEffect(statusEffectInfo.duration));
        }
    }

    private void AddTags()
    {
        foreach(string tag in addingTags)
        {
            owner.GetComponent<GameplayTagSystem>().AddTag(tag);
            //Debug.Log(tag + " tag added!");
        }
    }
    
    public void RemoveTags()
    {
        foreach(string tag in addingTags)
        {
            owner.GetComponent<GameplayTagSystem>().RemoveTag(tag);
            //Debug.Log(tag + " tag removed!");

        }
    }

    public void ApplyAttributeEffects(bool addEffects)
    {
        float localMult = addEffects ? 1 : -1;
        foreach(AttributeChange ac in attributeEffects)
        {
            owner.GetComponent<AttributeSystem>().ChangeAttributeValue(ac.attributeName, ac.changeAmount*localMult, ac.changeType);
        }
    }

    public void ClearTimers()
    {
        applyPeriodicEffects = false;
        StopAllCoroutines();
    }

    virtual public IEnumerator PeriodicEffectApplication(float seconds)
    {
        while(applyPeriodicEffects)
        {
            //apply effects
            yield return new WaitForSeconds(seconds);
        }
    }

    public void ForceRemoveStatusEffect()
    {
        StartCoroutine(EndStatusEffect(0));
    }

    private IEnumerator EndStatusEffect(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        AttemptRemoveStatusEffect();
        applyPeriodicEffects = false;
        Destroy(gameObject);
        Destroy(this);
    }

    private void AttemptRemoveStatusEffect()
    {
        owner.GetComponent<StatusEffectSystem>().RemoveStatusEffect(this);
    }

}
