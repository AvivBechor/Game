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
        if (Input.GetMouseButtonDown(LEFT_CLICK) && !player.isAttacking && !player.isInteracting && !player.isDead && player.CurrentRecource.value >= player.character.attackRecourseCost)
        {
            player.CurrentRecource.value -= player.character.attackRecourseCost;
            //Set the player to be attacking and stop his walking animation
            player.isAttacking = true;
            player.isMoving = false;
            //Create the attack object on the player
            GameObject o = Object.Instantiate(attack.attack, transform.position, transform.rotation);
            //Change the attack object's position based on the player's rotation
            o.transform.position += player.getRotationVector();
            //Bind the attack object to the player that cast it.
            o.GetComponent<Attack>().parent = GetComponent<Player>();
        }
    }
}
