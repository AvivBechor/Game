using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Vroom : Attack
{
    public readonly static float coolDown = 1.2f;
    private Object[] sprites;
    private float spritecum = 0;
    private int spriteindex;
    protected override int calculateDamage()
    {
        return (int)player.getModStatValue("Strength");
    }

    public override void SpawnAttack(string attackName, Side direction, int uuid)
    {
        base.SpawnAttack(attackName, direction, uuid);
        this.uuid = uuid;
        this.attackName = attackName;
        this.direction = direction;
        this.spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        this.speed = 2;
        this.lifeSpan = 2f;
        sprites = Resources.LoadAll(@"Attacks\starSprite", typeof(Sprite));

    }

    public override void SpawnAttackHeadless(Player player, string attackName)
    {
        base.SpawnAttackHeadless(player, attackName);
        this.speed = 2;
        this.lifeSpan = 2f;
    }

    private new void Update()
    {
        base.Update();
        spritecum += Time.deltaTime;
        if(spritecum >= 1/60)
        {
            spritecum = 0;
            spriteRenderer.sprite = (Sprite)(sprites[spriteindex]);
            spriteindex = (++spriteindex % 59);
        }
    }
}
