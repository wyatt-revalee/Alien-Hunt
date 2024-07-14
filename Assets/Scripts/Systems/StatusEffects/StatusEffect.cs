using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public abstract class StatusEffect : MonoBehaviour
{
    public GameObject owner;
    public List<string> requiredTags;
    public List<string> preventingTags;
    public List<string> addingTags;
    public List<AttributeChange> AttributeEffects;
    void Start()
    {
        
    }

    public void AttemptApplication()
    {
        if(!CheckTagStartConditions())
        {
            return;
        }
        //call effect add
        // start status effect
    }

    public bool CheckTagStartConditions()
    {
        List<string> activeTags = owner.GetComponent<GameplayTagSystem>().GetActiveTags();
        return false;
    }

    private void StartStatusEffect()
    {
        AddTags();
        ApplyAttributeEffects(true);
        // set timer for periodic application
        // set timer for status removal
    }

    private void AddTags()
    {
        foreach(string tag in addingTags)
        {
            owner.GetComponent<GameplayTagSystem>().AddTag(tag);
            Debug.Log(tag + " tag added!");
        }
    }
    
    public void RemoveTags()
    {
        foreach(string tag in addingTags)
        {
            owner.GetComponent<GameplayTagSystem>().RemoveTag(tag);
        }
    }

    public void ApplyAttributeEffects(bool addEffects)
    {
        float localMult = addEffects ? 1 : 0;
        foreach(AttributeChange ac in AttributeEffects)
        {
            owner.GetComponent<AttributeSystem>().ChangeAttributeValue(ac.attributeName, ac.changeAmount, ac.changeType);
        }
    }

    public void ClearTimers()
    {

    }

}
