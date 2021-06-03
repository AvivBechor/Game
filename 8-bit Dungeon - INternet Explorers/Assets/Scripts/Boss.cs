using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
public class Boss : Enemy
{
    void Start()
    {
        GameObject music = GameObject.FindGameObjectWithTag("music");
        Destroy(music);
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = Resources.Load("Boss") as AudioClip;
        audioSource.Play();
    }
    public override void Init(string name)
    {
        HP = 200;
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
