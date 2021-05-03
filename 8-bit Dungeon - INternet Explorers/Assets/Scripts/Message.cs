using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message
{
    public string command;
    public int uuid;
    public string data;
    
    public Message(string command, int uuid, string data)
    {
        this.command = command;
        this.uuid = uuid;
        this.data = data;
    }
}
