using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message
{
    public string command;
    public int uuid;
    public List<string> data;
    
    public Message(string command, int uuid, string data)
    {
        this.data = new List<string>();
        this.command = command;
        this.uuid = uuid;
        string[] asArray = data.Split('/');
        foreach(string dat in asArray) {
            this.data.Add(dat);
        }
    }
}
