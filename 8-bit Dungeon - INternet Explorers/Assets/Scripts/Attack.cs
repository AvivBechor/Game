using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public abstract class Attack : MonoBehaviour
{
    public int damage;
    public Player player;
    public string attackName;
    public SpriteRenderer spriteRenderer;
    public Side direction;

    public virtual void SpawnAttack(Player player, string attackName, Side direction)
    {
        this.player = player;
        this.name = attackName;
        this.direction = direction;
        this.damage = calculateDamage();
        this.spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        switch(direction)
        {
            case Side.UP:
                /*this.gameObject.transform.Rotate(new Vector3(0, 0, 0), Space.World);*/
                break;
            case Side.LEFT:
                this.gameObject.transform.Rotate(new Vector3(0, 0, 90), Space.World);
                break;
            case Side.DOWN:
                this.gameObject.transform.Rotate(new Vector3(0, 0, 180), Space.World);
                break;
            case Side.RIGHT:
                this.gameObject.transform.Rotate(new Vector3(0, 0, 270), Space.World);
                break;
            default:
                throw new System.Exception();
        }
        spriteRenderer.sprite = Resources.Load("ATK_" + attackName, typeof(Sprite)) as Sprite;
    }
    protected abstract int calculateDamage();
}
