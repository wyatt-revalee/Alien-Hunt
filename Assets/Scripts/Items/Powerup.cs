using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public StatusEffect currentEffect;
    public List<StatusEffect> statusEffects = new List<StatusEffect>();
    void Start()
    {
        ChooseRandomEffect();
    }

    private void ChooseRandomEffect()
    {
        currentEffect = statusEffects[Random.Range(0, statusEffects.Count)];
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        currentEffect.owner = collider2D.transform.gameObject;
        currentEffect.owner.GetComponent<StatusEffectSystem>().AddStatusEffect(currentEffect);
        Destroy(gameObject);
    }
}
