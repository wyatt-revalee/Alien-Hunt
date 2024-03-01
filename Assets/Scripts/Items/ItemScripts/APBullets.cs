using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APRounds : Item
{
    public override string GiveName()
    {
        return "AP Rounds";
    }

    public override string GiveId()
    {
        return "ap_rounds";
    }

    public override void UpdatePlayer(Player player, int stacks)
    {
        player.stats["DamageFlat"].value = player.baseStats.damageModiferFlat.value + (1 * stacks);
    }
}
