using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace StreamServer.RTSP
{
    public class RtspModel
    {
        public int ServerPort { get; set; }
        public IPAddress ServerIP { get; set; }
        public Socket tcpServer { get; set; } = new Socket(
            AddressFamily.InterNetwork,
            SocketType.Stream,
            ProtocolType.Tcp);

        public RtspModel(int serverPort, IPAddress serverIP)
        {
            this.ServerPort = serverPort;
            this.ServerIP = serverIP;

            var listenEndPoint = new IPEndPoint(ServerIP, this.ServerPort);

            //bind the server socket to the server IP and port and start listening
            tcpServer.Bind(listenEndPoint);
            tcpServer.Listen(int.MaxValue);
        }

        public Socket AcceptOneClient() => tcpServer.Accept();
    }
}
