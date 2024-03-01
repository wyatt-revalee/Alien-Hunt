using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Increases player's health by 5 for each stack
public class HealthUpgrade : Item
{
    public override string GiveName()
    {
        return "Health Upgrade";
    }
    public override string GiveId()
    {
        return "health_upgrade";
    }

    public override void UpdatePlayer(Player player, int stacks)
    {
        player.stats["MaxHealth"].value = player.baseStats.maxHealth.value + ( 5 * stacks);
    }
}
