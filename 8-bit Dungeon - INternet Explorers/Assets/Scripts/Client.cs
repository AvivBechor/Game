﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;

public class Client : MonoBehaviour
    
{
    public Player player;
    public GameObject test;
    public Queue<Message> messages;
    private int port = 5555;
    private string ip = "127.0.0.1";
    private readonly int HEADER = 4;
    private Socket s;
    private bool isRecieving = false;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("SPEGHETTI START");
        GameObject a = GameObject.Instantiate(test, Vector3.zero, Quaternion.identity);
        a.AddComponent<Strike>();
        a.GetComponent<Strike>().SpawnAttack(player, "test", Assets.Scripts.Side.DOWN);
        Debug.Log("SPEGHETTI END");
        messages = new Queue<Message>();

        s = new Socket(SocketType.Stream, ProtocolType.Tcp);
        s.Connect(ip, port);
        Debug.Log("Connected");
        sendMessage("crt", 111, 3, "warrior/1", s,HEADER);
        string msg=recvMessage((s,HEADER));
        Debug.Log(msg);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Task<string> msgTask = recvMessageAsync((s, HEADER));
        string msg = await msgTask;
        Debug.Log("WE RECIEVED: " + msg);
        */
        if (!isRecieving)
        {
            isRecieving = true;
            string msg = recvMessage((s, HEADER));
            Debug.Log("RECIEVED: " + msg);
            try
            {
                messages.Enqueue(new Message(msg.Split(':')[0], int.Parse(msg.Split(':')[1]), msg.Split(':')[2]));
            }
            catch
            {
                Debug.Log("wtf: " + msg);
            }
        }
    }
    public static bool sendMessage(string cmd, int gameID, int userID, string msg, Socket s, int HEADER)
    {
        try
        {
            msg = cmd + ":" + gameID + ":" + userID + ":" + msg;
            int len = msg.Length;

            for (int i = len.ToString().Length; i < HEADER; i++)
            {
                msg = " " + msg;
            }
            msg = len.ToString() + msg;
            int byteCount = Encoding.UTF8.GetByteCount(msg);
            byte[] sendData = new byte[byteCount];
            sendData = Encoding.UTF8.GetBytes(msg);
            if (msg.Length % 2 != 0)
            {
                msg += "~";
            }
            s.Send(sendData);
            return true;
        }
        catch
        {
            Debug.Log("not connected");
            return false;
        }
    }
    public string recvMessage(System.Object obj)
    {
        
        (Socket, int) t = ((Socket,int))obj;
        Socket s = t.Item1;
        int HEADER = t.Item2;
        s.ReceiveTimeout = 10;

        try
        {
            bool new_msg = true;
            string full_msg = "";
            string message = "";
            int msg_len = 0;
            while (true)
            {
                byte[] msg = new byte[2];
                s.Receive(msg);
                message = Encoding.UTF8.GetString(msg, 0, msg.Length);
                if (new_msg)
                {
                    msg_len = int.Parse(message.Replace(" ", ""));
                    new_msg = false;
                }
                full_msg += message;
                int full_msg_len = full_msg.Replace("~", "").Length;
                if (full_msg_len - HEADER == msg_len)
                {
                    new_msg = true;
                    isRecieving = false;
                    return full_msg.Substring(HEADER, msg_len);
                }

            }
        }
        catch
        {
            isRecieving = false;
            return "not connected";
        }
    }

}
