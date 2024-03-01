using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : Item
{
    public override string GiveName()
    {
        return "Weapon Item";
    }

    public override string GiveId()
    {
        return "weapon_item";
    }
}
