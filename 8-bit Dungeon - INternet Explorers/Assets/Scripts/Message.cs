using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message
{
    string command;
    string id;
    string data;

    public Message(string command, string data)
    {
        this.command = command;
        this.data = data;
    }
}
