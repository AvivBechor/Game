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

    public override void SpawnAttack(string attackName, Side direction, int uuid)
    {
        base.SpawnAttack(attackName, direction, uuid);
        this.speed = 3;
        this.lifeSpan = 2f;
    }

    public override void SpawnAttackHeadless(Player player, string attackName, Side direction)
    {
        base.SpawnAttackHeadless(player, attackName, direction);
        this.speed = 0;
        this.lifeSpan = 0.5f;
    }
}
