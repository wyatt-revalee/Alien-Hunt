using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class SE_MoneyTome : StatusEffect
{

    public GameObject coinEffect;
    public override void InitializeEffects()
    {
        statusEffectInfo = new StatusEffectInfo("Money Tome", 0f, 0f, "Adds chance to receive money on kill", "SE_MoneyTome", false, icon);
    }

    public override void AttemptApplication()
    {
        bool effectFound = false;
        foreach (StatusEffect effect in owner.GetComponent<StatusEffectSystem>().ActiveStatusEffects)
        {
            if (effect.statusEffectInfo.ID == "SE_MoneyTome")
            {
                effect.GetComponent<SE_MoneyTome>().stacks++;
                effectFound = true;
                Destroy(gameObject);
            }
        }
        if (!effectFound)
        {
            base.AttemptApplication();
            owner.GetComponent<Enemy>().onDeath += AttemptDropMoney;
            if(owner.GetComponent<Enemy>().GetAttributeValue("health") <= 0)
            {
                //Debug.Log("Stacks: " + stacks);
                AttemptDropMoney();
            }
        }
    }

    void AttemptDropMoney()
    {
        int chance = 10 * stacks;
        int roll = UnityEngine.Random.Range(1, 101);
        //Debug.Log(string.Format("Chance: {0} | Roll: {1}", chance, roll));
        if(roll <= chance)
        {
            GameObject dropEffect = Instantiate(coinEffect, owner.transform);
            dropEffect.SetActive(true);
            applier.GetComponent<Player>().AddCoins(1);
        }
    }

}
