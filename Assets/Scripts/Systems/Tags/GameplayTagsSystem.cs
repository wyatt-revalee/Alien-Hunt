using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameplayTagSystem : MonoBehaviour
{

    Dictionary<string, int> StackedTags;

    public void AddTag(string tagToAdd)
    {
        AddStack(tagToAdd);
    }

    public void RemoveTag(GameplayTag tagToRemove)
    {
        RemoveStack(tagToRemove);
    }

    public List<string> GetActiveTags()
    {
        return StackedTags.Keys.ToList();
    }

    public void AddStack(string tagToAdd)
    {
        StackedTags[tagToAdd]++;
    }

    public void RemoveStack(GameplayTag tagToAdd)
    {
        StackedTags[tagToAdd.tagName]--;
    }

    public bool HasAllTags(List<string> tagsToCheck)
    {
        foreach (string tag in tagsToCheck)
        {
            if (!StackedTags.ContainsKey(tag))
            {
                return false;
            }
        }
        return true;
    }

    public bool HasAnyTags(List<string> tagsToCheck)
    {
        foreach(string tag in tagsToCheck)
        {
            if(StackedTags.ContainsKey(tag))
            {
                return true;
            }
        }
        return false;
    }
}
