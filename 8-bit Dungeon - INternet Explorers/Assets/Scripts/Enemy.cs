using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Enemy : MonoBehaviour
{
    public string name;
    public Dictionary<string, Stat> stats;
    public int level;
    public Side rotation;
    public Sprite spriteUP;
    public Sprite spriteDOWN;
    public Sprite spriteLEFT;
    public Sprite spriteRIGHT;
    public int HP;
    public virtual void Init(string name)
    {
        this.rotation = Side.DOWN;
        this.name = name;       
    }

}
