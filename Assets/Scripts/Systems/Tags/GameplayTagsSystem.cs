using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameplayTagSystem : MonoBehaviour
{

    public Dictionary<string, int> stackedTags = new Dictionary<string, int>();

    public void AddTag(string tagToAdd)
    {
        AddStack(tagToAdd);
    }

    public void RemoveTag(string tagToRemove)
    {
        RemoveStack(tagToRemove);
    }

    public List<string> GetActiveTags()
    {
        return stackedTags.Keys.ToList();
    }

    public void AddStack(string tagToAdd)
    {
        if(stackedTags.ContainsKey(tagToAdd))
        {
            stackedTags[tagToAdd]++;
        }
        else
        {
            stackedTags[tagToAdd] = 1;
        }
    }

    public void RemoveStack(string tagToAdd)
    {
        if (stackedTags[tagToAdd] == 1)
        {
            stackedTags.Remove(tagToAdd);
        }
        else
        {
            stackedTags[tagToAdd]--;
        }
    }

    public bool HasAllTags(List<string> tagsToCheck)
    {
        foreach (string tag in tagsToCheck)
        {
            if (!stackedTags.ContainsKey(tag))
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
            if(stackedTags.ContainsKey(tag))
            {
                return true;
            }
        }
        return false;
    }
}
