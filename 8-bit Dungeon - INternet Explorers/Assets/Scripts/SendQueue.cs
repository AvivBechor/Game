using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendQueue : MonoBehaviour
{
    private Client client;
    private Queue<string> messages;
    int count = 0;
    void Start()
    {
        client = GameObject.Find("Connection").GetComponent<Client>();
        messages = new Queue<string>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        string test = "mistake";
        if(messages.Count > 0)
        {
            
            string currentMessage = messages.Dequeue();
            test = currentMessage;            
            client.sendMessage(currentMessage.Split(':')[0], int.Parse(currentMessage.Split(':')[1]), int.Parse(currentMessage.Split(':')[2]),currentMessage.Split(':')[3],client.s,4);
        }
        else if (messages.Count == 0)
        {
            int gameID = GameObject.Find("Connection").GetComponent<gameIDHandler>().gameID;
            test = "nul";
            client.sendMessage("nul", gameID, -1, count.ToString(), client.s, 4);
            count++;
        }
        Debug.Log("the sent message is " + test);
    }

    public void addMessage(string message)
    {
        messages.Enqueue(message);
    }
}
