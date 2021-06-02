using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Player player;
    private PlayerMovement playerMovement;
    private const int LEFT_CLICK = 0;
    public SendQueue sendQueue;
    private float accum;
    private int gameID;
    private int playerUUID;
    public IntStorage uuidHolder;
    public IntStorage gameIDHolder;



    void Start()
    {
        player = GetComponent<Player>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        if (!player.singlePlayer)
        {
            playerUUID = uuidHolder.value;
            gameID = gameIDHolder.value;
        }
    }

    void FixedUpdate()
    {
        if(player.inGame)
        {
            if (player.isAttacking) {
                accum += Time.deltaTime;
                if (accum >= Strike.coolDown && player.character.title.ToLower().Equals("warrior"))
                {
                    accum = 0;
                    player.isAttacking = false;
                }
                else if(accum >= Vroom.coolDown && player.character.title.ToLower().Equals("mage"))
                {
                    accum = 0;
                    player.isAttacking = false;
                }
            }
            
            if (Input.GetMouseButton(LEFT_CLICK) && player.isAttacking == false)
            {
                player.isAttacking = true;
                player.isMoving = false;
                //(float, float) dir = (0, 0);
                float x = 0;
                float y = 0;
                if(player.rotation == Side.UP)
                {
                    //dir = (0, 1);
                    x = 0f;
                    y = 1f;
                }
                else if(player.rotation == Side.LEFT)
                {
                    //dir = (-1, -0.25f);
                    x = -1;
                    y = 0;
                }
                else if(player.rotation == Side.DOWN)
                {
                    //dir = (0, -1);
                    x = 0;
                    y = -1.25f;
                }
                else if(player.rotation == Side.RIGHT)
                {
                    //dir = (1, 0.25f);
                    x = 1;
                    y = 0;
                }
                Vector3 pos = new Vector3(player.transform.position.x + x, player.transform.position.y + y, player.transform.position.z);
                if(player.character.title.ToLower() == "warrior")
                {
                    Strike strike = new Strike();
                    strike.SpawnAttackHeadless(player, "strike");
                    sendQueue.addMessage("atk:" + player.GetComponent<gameIDHandler>().gameID + ":" + player.GetComponent<UUIDHandler>().UUID + ":" + pos.x + "," + pos.y + "/" + strike.damage + "/" + strike.speed + "/" + player.rotation.ToString() + "/" + strike.lifeSpan + "/strike");
                }
                else if(player.character.title.ToLower() == "mage")
                {
                    Vroom vroom = new Vroom();
                    vroom.SpawnAttackHeadless(player, "vroom");
                    sendQueue.addMessage("atk:" + player.GetComponent<gameIDHandler>().gameID + ":" + player.GetComponent<UUIDHandler>().UUID + ":" + pos.x + "," + pos.y + "/" + vroom.damage + "/" + vroom.speed + "/" + player.rotation.ToString() + "/" + vroom.lifeSpan + "/vroom");
                }
            }
            
        }
    }
}
