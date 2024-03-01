// Made with help from https://www.youtube.com/watch?v=iU6mKyQjOYI

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Item : MonoBehaviour
{

    public ItemData itemData;
    public abstract string GiveName();
    public abstract string GiveId();
    public virtual void UpdatePlayer(Player player, int stacks)
    {
    }

    public virtual void UpdateEnemy(IDamageable enemy, int stacks)
    {
    }

    public virtual void OnHit(Player player, IDamageable enemy, int stacks)
    {
    }

    public virtual bool IsBuff()
    {
        return false;
    }

    public virtual int GiveBuffTime()
    {
        return 0;
    }

}

