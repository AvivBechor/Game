using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

public class Client : MonoBehaviour
{
    NetworkStream stream;
    byte[] sendData;
    int byteCount;
    TcpClient client;
    public string message;
    string ip = "192.168.1.18";
    int port = 5555;
    byte[] data;
    public Queue<string> instructions;

    // Start is called before the first frame update
    void Start()
    {
        client = new TcpClient(ip, port);
        byteCount = 5;
        stream = client.GetStream();
        //stream.Write(sendData, 0, sendData.Length);
        data = new byte[byteCount];
        instructions = new Queue<string>();
    }

    // Update is called once per frame
     void Update()
    {
        //byteCount = Encoding.UTF8.GetByteCount("hello wrodl");
       
        stream.Read(data,0,data.Length);
        message=Encoding.Default.GetString(data);
        instructions.Enqueue(message);
        Debug.Log(message);
    }
}
