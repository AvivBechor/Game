using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strike : Attack
{
    protected override int calculateDamage()
    {
        return (int)player.getModStatValue("Strength");
    }
}
