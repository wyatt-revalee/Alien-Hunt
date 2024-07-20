using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerupRemover : MonoBehaviour
{
    public GameObject currentEffect;
    private StatusEffect effectScript;
    public List<GameObject> statusEffects = new List<GameObject>();
    void Start()
    {
        ChooseRandomEffect();
    }

    private void ChooseRandomEffect()
    {
        currentEffect = statusEffects[Random.Range(0, statusEffects.Count)];
        currentEffect = Instantiate(currentEffect);
        effectScript = currentEffect.GetComponent<StatusEffect>();
        effectScript.InitializeEffects();
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.gameObject.layer == 6)
        {
            effectScript.owner = collider2D.transform.gameObject;
            effectScript.owner.GetComponent<StatusEffectSystem>().RemoveStatusEffect(effectScript);
            Destroy(gameObject);
        }
    }
}
