using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRateUpgrade : Item
{
    public override string GiveName()
    {
        return "Fire Rate Upgrade";
    }

    public override string GiveId()
    {
        return "fire_rate_upgrade";
    }

    public override void UpdatePlayer(Player player, int stacks)
    {
        player.stats["FireRate"].value = player.baseStats.fireRateModifier.value + (0.1f * stacks);
    }
}
