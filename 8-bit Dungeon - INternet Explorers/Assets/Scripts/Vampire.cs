using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Vampire : Enemy
{
    public override void Init(string name)
    {
        base.Init(name);
        this.stats = new Dictionary<string, Stat>();
        stats.Add("Movespeed", new Stat(1, 0));
        Object[] sprites = Resources.LoadAll(@"MightyPack and more\MV\Characters\NPC_Evil");
        spriteUP = (Sprite)sprites[46];
        spriteRIGHT = (Sprite)sprites[34];
        spriteLEFT = (Sprite)sprites[22];
        spriteDOWN = (Sprite)sprites[7];
    }
}
