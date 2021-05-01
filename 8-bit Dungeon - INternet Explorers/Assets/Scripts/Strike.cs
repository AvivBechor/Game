using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strike : Attack
{
    public override void Spawn()
    {
        /*TODO: Generate UID*/
        lifespan = 0.5f;
        speed = 0;
        damage = calculateDamage();
    }

    protected override int calculateDamage()
    {
        return (int)(player.character.stats["Strength"].value + player.character.stats["Strength"].mod);
    }
}
