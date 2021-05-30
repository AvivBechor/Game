using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    public override void Init(string name)
    {
        base.Init(name);
        this.stats = new Dictionary<string, Stat>();
        stats.Add("Movespeed", new Stat(1, 0));
        Object[] sprites = Resources.LoadAll(@"MightyPack and more\MV\Characters\NPC_Evil");
        spriteUP = (Sprite)sprites[37];
        spriteRIGHT = (Sprite)sprites[25];
        spriteLEFT = (Sprite)sprites[13];
        spriteDOWN = (Sprite)sprites[1];
    }
}
