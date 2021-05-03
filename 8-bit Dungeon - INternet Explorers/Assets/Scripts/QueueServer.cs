using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueServer : MonoBehaviour
{
    public Queue<Message> messages;

    // Update is called once per frame
    void Update()
    {
        messages = GameObject.Find("Connection").GetComponent<Client>().messages;
        if (messages.Count > 0)
        {
            Message currentMessage = messages.Dequeue();
            Debug.Log("message.cmd" + currentMessage.command + ", uuid " + currentMessage.uuid + " val " + currentMessage.data);
            if (currentMessage.command == "mov")
            {
                GameObject players = GameObject.Find("PlayersContainer");
                foreach (Transform child in players.transform)
                {
                    if (child.gameObject.GetComponent<UUIDHandler>().UUID == currentMessage.uuid)
                    {
                        Debug.Log("name:" + child.gameObject.name);
                        child.GetComponent<PlayerMovement>().xMovement = float.Parse(currentMessage.data.Split(',')[0]);
                        child.GetComponent<PlayerMovement>().yMovement = float.Parse(currentMessage.data.Split(',')[1]);
                        break;
                    }
                }
            }
        }
    }
}
