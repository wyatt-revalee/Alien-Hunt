using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendedMags : Item
{
    public override string GiveName()
    {
        return "Extended Mags";
    }

    public override string GiveId()
    {
        return "extended_mags";
    }

    public override void UpdatePlayer(Player player, int stacks)
    {
        player.stats["MagazineSize"].value = player.baseStats.magazineSizeModifier.value + (0.25f * stacks);
    }
}
