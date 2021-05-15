using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public abstract class Attack : MonoBehaviour
{
    public int speed;
    public float lifeSpan;
    public int damage;
    public Player player;
    public string attackName;
    public SpriteRenderer spriteRenderer;
    public Side direction;
    public int uuid;
    public bool isHeadless = true;
    private float timePassed;
    public virtual void SpawnAttackHeadless(Player player, string attackName, Side direction)
    {
        this.player = player;
        this.attackName = attackName;
        this.direction = direction;
        this.damage = calculateDamage();
        //this.spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
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
        //Debug.Log(@"Attacks\ATK_" + attackName.ToUpper());
        //Sprite spr = Resources.Load(@"Attacks\ATK_" + attackName.ToUpper(), typeof(Sprite)) as Sprite;
        //Debug.Log(spr.name);

        //spriteRenderer.sprite = spr;
    }

    public virtual void SpawnAttack(string attackName, Side direction, int uuid)
    {
        this.uuid = uuid;
        this.attackName = attackName;
        this.direction = direction;
        this.spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        switch (direction)
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
        Sprite spr = Resources.Load(@"Attacks\ATK_" + attackName.ToUpper(), typeof(Sprite)) as Sprite;
        spriteRenderer.sprite = spr;
    }

    public void Update()
    {
        if(!isHeadless)
        {
            timePassed += Time.deltaTime;
            if(timePassed >= lifeSpan)
            {
                Destroy(gameObject);                
            }
            var step = speed * Time.deltaTime;
            switch (direction)
            {
                case Side.UP:
                    transform.position += new Vector3(0, step, 0);
                    break;
                case Side.DOWN:
                    transform.position += new Vector3(0, -step, 0);
                    break;
                case Side.RIGHT:
                    transform.position += new Vector3(step, 0, 0);
                    break;
                case Side.LEFT:
                    transform.position += new Vector3(-step, 0, 0);
                    break;
            }
        }
    }
    protected abstract int calculateDamage();
}
