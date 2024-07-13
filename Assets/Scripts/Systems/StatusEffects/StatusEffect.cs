using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class StatusEffect : MonoBehaviour
{
    public GameObject owner;
    public List<string> requiredTags;
    public List<string> preventingTags;
    public List<string> addingTags;
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

    public void ApplyAttributeEffects(bool addEffects)
    {
        float localMult = addEffects ? 1 : 0;
    }

    public void ClearTimers()
    {

    }

}
