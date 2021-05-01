using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public abstract class Attack : MonoBehaviour
{
    public int UID;
    public float damage;
    public float lifespan;
    public float speed;
    public Side direction;
    protected GameObject attacksContainer;
    protected Player player;

    public void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    public abstract void Spawn();
    protected abstract int calculateDamage();
}
