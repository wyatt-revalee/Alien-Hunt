using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilverBullets : Item
{
    public override string GiveName()
    {
        return "Silver Bullets";
    }

    public override string GiveId()
    {
        return "silver_bullets";
    }

    public override void UpdatePlayer(Player player, int stacks)
    {
        player.stats["DamagePercent"].value = player.baseStats.damageModifierPercentage.value + (0.05f * stacks);
    }
}
