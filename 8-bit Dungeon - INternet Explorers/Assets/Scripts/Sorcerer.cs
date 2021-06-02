using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Sorcerer : Enemy
{
    public override void Init(string name)
    {
        base.Init(name);
        HP = 100;
        this.stats = new Dictionary<string, Stat>();
        stats.Add("Movespeed", new Stat(1, 0));
        Object[] sprites = Resources.LoadAll(@"MightyPack and more\MV\Characters\NPC_Evil");
        spriteUP = (Sprite)sprites[93];
        spriteRIGHT = (Sprite)sprites[81];
        spriteLEFT = (Sprite)sprites[69];
        spriteDOWN = (Sprite)sprites[59];
    }
}
