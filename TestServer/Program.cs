using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace TestServer
{
    class Program
    {
        static void Main(string[] args)
        {
            int port=5555;
            string ip= "192.168.1.18";
            string message="this is a test";
            NetworkStream stream;
            byte[] sendData;
            int byteCount;
            TcpClient client;
            client = new TcpClient(ip, port);
            byteCount = Encoding.UTF8.GetByteCount(message);
            sendData = new byte[byteCount];
            sendData = Encoding.UTF8.GetBytes(message);
            stream = client.GetStream();
            stream.Write(sendData, 0, sendData.Length);

        }
    }
}
