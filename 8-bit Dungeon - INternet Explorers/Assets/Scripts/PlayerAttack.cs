using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Player player;
    private const int LEFT_CLICK = 0;
    public SendQueue sendQueue;

    void Start()
    {
        player = GetComponent<Player>();
        
    }

    void Update()
    {
        if(player.inGame)
        {
            if(Input.GetMouseButton(LEFT_CLICK))
            {
                (int, int) dir = (0, 0);
                if(player.rotation == Side.UP)
                {
                    dir = (0, 1);
                }
                else if(player.rotation == Side.LEFT)
                {
                    dir = (-1, 0);
                }
                else if(player.rotation == Side.DOWN)
                {
                    dir = (0, -1);
                }
                else
                {
                    dir = (1, 0);
                }
                Vector3 pos = new Vector3(player.transform.position.x + dir.Item1, player.transform.position.y + dir.Item2, player.transform.position.z);
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
