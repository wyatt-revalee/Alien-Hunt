using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.LowLevel;


public abstract class StatusEffect : MonoBehaviour
{
    public GameObject applier; // Object that applied effect (player/enemy)
    public GameObject owner;  // Owner of the object (who it will apply to)
    public Sprite icon;
    public List<string> requiredTags;
    public List<string> preventingTags;
    public List<string> addingTags;
    public bool applyPeriodicEffects;
    public List<AttributeChange> attributeEffects = new List<AttributeChange>();
    public StatusEffectInfo statusEffectInfo;
    public string type;
    public int stacks = 1;

    public virtual void AttemptApplication()
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

    public virtual void StartStatusEffect()
    {
        AddTags();
        ApplyAttributeEffects(true);
        applyPeriodicEffects = true;
        if(statusEffectInfo.periodicTime > 0)
        {
            StartCoroutine(PeriodicEffectApplication(statusEffectInfo.periodicTime));
        }
        if(statusEffectInfo.duration > 0)
        {
            // add logic concering buff/debuff attribute effects here
            if(statusEffectInfo.isDebuff)
            {
                StartCoroutine(EndStatusEffect(statusEffectInfo.duration));
            }
            else
            {
                StartCoroutine(EndStatusEffect(statusEffectInfo.duration + owner.GetComponent<AttributeSystem>().attributes["buffTime"].baseValue));
            }
        }
    }

    public void AddTags()
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

    public void AttemptRemoveStatusEffect()
    {
        owner.GetComponent<StatusEffectSystem>().RemoveStatusEffect(this);
    }

}
