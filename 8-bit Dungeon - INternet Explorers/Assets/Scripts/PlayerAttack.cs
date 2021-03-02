using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Player player;
    private const int LEFT_CLICK = 0;
    public AttackHolder attack;
    void Start()
    {
        player = GetComponent<Player>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(LEFT_CLICK) && !player.isAttacking)
        {
            //Set the player to be attacking and stop his walking animation
            player.isAttacking = true;
            player.isMoving = false;
            //Create the attack object on the player
            GameObject o = Object.Instantiate(attack.attack, transform.position, transform.rotation);
            //Change the attack object's position based on the player's rotation
            switch(player.rotation)
            {               
                case Side.UP:
                    o.transform.position += new Vector3(0, 1, 0);
                    break;
                case Side.DOWN:
                    o.transform.position += new Vector3(0, -1, 0);
                    break;
                case Side.LEFT:
                    o.transform.position += new Vector3(-1, 0, 0);
                    break;
                case Side.RIGHT:
                    o.transform.position += new Vector3(1, 0, 0);
                    break;
            }
            //Bind the attack object to the player that cast it.
            o.GetComponent<Attack>().parent = GetComponent<Player>();
        }
    }
}
