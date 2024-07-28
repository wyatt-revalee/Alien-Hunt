using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EffectsDisplay : MonoBehaviour
{

    public GameObject effectUI;
    public Player player;

    Dictionary<StatusEffect, GameObject> effects = new Dictionary<StatusEffect, GameObject>();

    void Awake()
    {
        player.GetComponent<StatusEffectSystem>().StatusEffectWasAdded += AddEffect;
        player.GetComponent<StatusEffectSystem>().StatusEffectWasRemoved += RemoveEffect;
    }

    private void AddEffect(StatusEffect effectToAdd)
    {
        if(effects.ContainsKey(effectToAdd))
        {
            effects[effectToAdd].GetComponent<Effect_UI>().AddToStack();
        }
        else
        {
            GameObject newEffect = Instantiate(effectUI, transform);
            newEffect.GetComponent<Effect_UI>().image.sprite = effectToAdd.icon;
            effects.Add(effectToAdd, newEffect);
        }
    }

    private void RemoveEffect(StatusEffect effectToRemove)
    {
        effects[effectToRemove].GetComponent<Effect_UI>().RemoveFromStack();
        if(effects[effectToRemove].GetComponent<Effect_UI>().stackSize <= 0)
        {
            Destroy(effects[effectToRemove]);
            effects.Remove(effectToRemove);
        }
    }

}