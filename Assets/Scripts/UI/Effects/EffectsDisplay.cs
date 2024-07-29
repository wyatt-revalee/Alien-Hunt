using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EffectsDisplay : MonoBehaviour
{

    public GameObject effectUI;
    public Player player;

    Dictionary<string, GameObject> effects = new Dictionary<string, GameObject>();

    void Awake()
    {
        player.GetComponent<StatusEffectSystem>().StatusEffectWasAdded += AddEffect;
        player.GetComponent<StatusEffectSystem>().StatusEffectWasRemoved += RemoveEffect;
    }

    private void AddEffect(StatusEffect effectToAdd)
    {
        if(effectToAdd.statusEffectInfo.duration <= 0)
        {
            return;
        }
        if(effects.ContainsKey(effectToAdd.statusEffectInfo.ID))
        {
            effects[effectToAdd.statusEffectInfo.ID].GetComponent<Effect_UI>().AddToStack();
        }
        else
        {
            GameObject newEffect = Instantiate(effectUI, transform);
            newEffect.GetComponent<Effect_UI>().image.sprite = effectToAdd.icon;
            effects.Add(effectToAdd.statusEffectInfo.ID, newEffect);
        }
    }

    private void RemoveEffect(StatusEffect effectToRemove)
    {
        if (effectToRemove.statusEffectInfo.duration <= 0)
        {
            return;
        }
        effects[effectToRemove.statusEffectInfo.ID].GetComponent<Effect_UI>().RemoveFromStack();
        if(effects[effectToRemove.statusEffectInfo.ID].GetComponent<Effect_UI>().stackSize <= 0)
        {
            Destroy(effects[effectToRemove.statusEffectInfo.ID]);
            effects.Remove(effectToRemove.statusEffectInfo.ID);
        }
    }

}