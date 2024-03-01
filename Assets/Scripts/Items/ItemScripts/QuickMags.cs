using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickMags : Item
{
    public override string GiveName()
    {
        return "Quick Mags";
    }

    public override string GiveId()
    {
        return "quick_mags";
    }

    public override void UpdatePlayer(Player player, int stacks)
    {
        player.stats["ReloadSpeed"].value = player.baseStats.reloadSpeedModifier.value + (0.1f * stacks);
    }
}
