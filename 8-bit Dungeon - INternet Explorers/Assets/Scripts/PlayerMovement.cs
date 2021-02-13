using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class PlayerMovement : MonoBehaviour
{
    private Player player;
    public LayerMask obstacle;
    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.movePoint.position, player.moveSpeed * Time.deltaTime);
        if ((Vector3.Distance(transform.position, player.movePoint.position) <= 0.05f))
        {
            player.isMoving = false;
        }
        else
        {
            player.isMoving = true;
        }

        if (!player.isDead && !player.isMoving)
        {
            if (Input.GetKey("w"))
            {
                player.rotation = Side.UP;
                if (!Physics2D.OverlapCircle(player.movePoint.position + new Vector3(0f, 1f, 0f), .2f, obstacle))
                {

                    player.movePoint.transform.position += new Vector3(0f, 1f, 0f);
                }
                
            }
            else if (Input.GetKey("s"))
            {
                player.rotation = Side.DOWN;
                
                if (!Physics2D.OverlapCircle(player.movePoint.position + new Vector3(0f, -1f, 0f), .2f, obstacle))
                {
                    player.movePoint.transform.position += new Vector3(0f, -1f, 0f);
                }

            }
            else if (Input.GetKey("d"))
            {
                player.rotation = Side.RIGHT;            
                if (!Physics2D.OverlapCircle(player.movePoint.position + new Vector3(1f, 0f, 0f), .2f, obstacle))
                {
                    player.movePoint.transform.position += new Vector3(1f, 0f, 0f);
                }
            }
            else if (Input.GetKey("a"))
            {
                player.rotation = Side.LEFT;
                
                if (!Physics2D.OverlapCircle(player.movePoint.position + new Vector3(-1f, 0f, 0f), .2f, obstacle))
                {
                    player.movePoint.transform.position += new Vector3(-1f, 0f, 0f);
                }
            }
        }
    }
}
