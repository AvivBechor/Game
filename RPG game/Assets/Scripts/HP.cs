using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class HP : MonoBehaviour
{
    
    public const int MaxHP = 100;
    public int healthPoints;
    public LayerMask Trap;
    public Transform movePoint;
    public GameObject Player;
    private PlayerMovement playerMovement;
    private Side rotation;

    void Start()
    {
        playerMovement = Player.GetComponent<PlayerMovement>();
        healthPoints = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
       
       rotation = playerMovement.rotation;
       if(playerMovement.isMoving == false)
       {
            if (Physics2D.OverlapCircle(transform.position, 0.2f, Trap))
            {
                if (rotation == Side.UP)
                {
                    movePoint.position += new Vector3(0f, -1f, 0f);
                    healthPoints -= 10;
                }
                else if (rotation == Side.DOWN)
                {

                    movePoint.position += new Vector3(0f, 1f, 0f);
                    healthPoints -= 10;
                }
                else if (rotation == Side.LEFT)
                {
                    movePoint.position += new Vector3(1f, 0f, 0f);
                    healthPoints -= 10;
                }
                else if (rotation == Side.RIGHT)
                {
                    movePoint.position += new Vector3(-1f, 0f, 0f);
                    healthPoints -= 10;
                }
            }
        }
        if (Input.GetKeyDown("space"))
        {
            playerMovement.dead = false;
            healthPoints = MaxHP;
        }
        if (healthPoints <= 0)
        {
            Debug.Log("lol u died");
            playerMovement.dead = true;
        }
        Debug.Log(healthPoints);
    }
}
