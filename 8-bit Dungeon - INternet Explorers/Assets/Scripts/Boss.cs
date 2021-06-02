using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
public class Boss : Enemy
{
    public override void Init(string name)
    {
        HP = 200;
        transform.localScale = new Vector3(3, 3, 1);
        base.Init(name);
        this.stats = new Dictionary<string, Stat>();
        stats.Add("Movespeed", new Stat(2, 0));
        Object[] sprites = Resources.LoadAll(@"MightyPack and more\MV\Characters\Actors_1");
        spriteUP = (Sprite)sprites[91];
        spriteRIGHT = (Sprite)sprites[79];
        spriteLEFT = (Sprite)sprites[69];
        spriteDOWN = (Sprite)sprites[55];
    }
}
