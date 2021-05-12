using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class QueueServer : MonoBehaviour
{
    public Queue<Message> messages;
    public GameObject baseAttack;

    // Update is called once per frame
    void Update()
    {
        messages = GameObject.Find("Connection").GetComponent<Client>().messages;
        if (messages.Count > 0)
        {
            Message currentMessage = messages.Dequeue();
            Debug.Log("message.cmd " + currentMessage.command + ", uuid " + currentMessage.uuid + " val " + currentMessage.data);
            Player serverPlayer = GameObject.Find("OtherPlayer").GetComponent<Player>();
            switch (currentMessage.command)
            {
                case "mov":
                    serverMovementScript otherPlayerMovement = GameObject.Find("OtherPlayer").GetComponent<serverMovementScript>();
                    otherPlayerMovement.xMovement = int.Parse(currentMessage.data[0].Split(',')[0]);
                    otherPlayerMovement.yMovement = int.Parse(currentMessage.data[0].Split(',')[1]);
                    break;
                case "atk":
                    Side dir = Side.UP;
                    if(currentMessage.data[2].Equals("UP"))
                    {
                        dir = Side.UP;
                    }
                    else if(currentMessage.data[2].Equals("LEFT"))
                    {
                        dir = Side.LEFT;
                    }
                    else if(currentMessage.data[2].Equals("DOWN"))
                    {
                        dir = Side.DOWN; 
                    }
                    else if(currentMessage.data[2].Equals("RIGHT"))
                    {
                        dir = Side.RIGHT;
                    }
                    createAttack(
                        currentMessage.uuid,
                        (float.Parse(currentMessage.data[0].Split(',')[0]), float.Parse(currentMessage.data[0].Split(',')[1])),
                        currentMessage.data[1],
                        dir
                        );
                    break;
                case "srt":
                    serverPlayer.gameObject.SetActive(true);
                    serverPlayer.character = new Character();
                    serverPlayer.character.title = currentMessage.data[0];
                    serverPlayer.gender = currentMessage.data[1].Equals("1");
                    serverPlayer.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>(@"MightyPack and more\MV\Characters\Actors_2")[54];
                    //serverPlayer.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load(@"MightyPack and more\MV\Characters\Actors_2_54", typeof(Sprite)) as Sprite;
                    break;
                case "pos":
                    serverPlayer.transform.position = new Vector3(int.Parse(currentMessage.data[0].Split(',')[0]), int.Parse(currentMessage.data[0].Split(',')[1]), 83.19981f);
                    break;
            }
        }
    }

    void createAttack(int atkUUID, (float, float) pos, string atkName, Side direction)
    {
        GameObject a = GameObject.Instantiate(baseAttack, new Vector3(pos.Item1, pos.Item2, 0), Quaternion.identity);
        //TURN TO SWITCH STATEMENT OVER ATTACK NAME
        a.AddComponent<Strike>()
         .SpawnAttack(atkName, direction);
        //******************************************
        a.AddComponent<UUIDHandler>()
         .UUID = atkUUID;

    }

    GameObject getPlayerByUUID(int uuid)
    {
        GameObject players = GameObject.Find("PlayersContainer");
        foreach (Transform child in players.transform)
        {
            if (child.gameObject.GetComponent<UUIDHandler>().UUID == uuid)
            {
                return child.gameObject;
            }
        }
        return null;
    }
}
