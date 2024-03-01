using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSizeUpgrade : Item
{
    public override string GiveName()
    {
        return "Bullet Size Upgrade";
    }

    public override string GiveId()
    {
        return "bullet_size_upgrade";
    }

    public override void UpdatePlayer(Player player, int stacks)
    {
        player.stats["BulletSize"].value = player.baseStats.bulletSizeModifer.value + (0.1f * stacks);
    }
}
