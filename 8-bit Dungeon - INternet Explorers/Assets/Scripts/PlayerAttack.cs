using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Player player;
    private const int LEFT_CLICK = 0;
    public SendQueue sendQueue;
    private float accum;


    void Start()
    {
        player = GetComponent<Player>();
        
    }

    void Update()
    {
        if(player.inGame)
        {
            if (player.isAttacking) {
                accum += Time.deltaTime;
                if (player.character.title.ToLower() == "warrior" && accum >= Strike.coolDown)
                {
                    accum = 0;
                    player.isAttacking = false;
                }
            }
            
            if (Input.GetMouseButton(LEFT_CLICK) && player.isAttacking == false)
            {
                player.isAttacking = true;
                //(float, float) dir = (0, 0);
                float x;
                float y;
                if(player.rotation == Side.UP)
                {
                    //dir = (0, 1);
                    x = 0;
                    y = 1;
                }
                else if(player.rotation == Side.LEFT)
                {
                    //dir = (-1, -0.25f);
                    x = -1;
                    y = -0.25f;
                }
                else if(player.rotation == Side.DOWN)
                {
                    //dir = (0, -1);
                    x = 0;
                    y = -1;
                }
                else
                {
                    //dir = (1, 0.25f);
                    x = 1;
                    y = 0.25f;
                }
                Vector3 pos = new Vector3(player.transform.position.x + x, player.transform.position.y + y, player.transform.position.z);
                if(player.character.title.ToLower() == "warrior")
                {
                    Strike strike = new Strike();
                    strike.SpawnAttackHeadless(player, "strike");
                    sendQueue.addMessage("atk:" + player.GetComponent<gameIDHandler>().gameID + ":" + player.GetComponent<UUIDHandler>().UUID + ":" + pos.x + "," + pos.y + "/" + strike.damage + "/" + strike.speed + "/" + player.rotation.ToString() + "/" + strike.lifeSpan + "/strike");
                }
            }
            
        }
    }
}
