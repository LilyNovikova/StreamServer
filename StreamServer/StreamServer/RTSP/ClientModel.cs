using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace StreamServer.RTSP
{
    public class ClientModel
    {
        //byte array to store request and response messages between client and server
        private byte[] buffer;
        private Socket clientSocket = null;
        public int sessionNum { get; set; }

        //timer used to send the images at the video frame rate
        public System.Timers.Timer timer = new System.Timers.Timer();

        public ClientModel(Socket clientSocket)
        {
            this.clientSocket = clientSocket;
            // set session ID as a random number between 7000 and 8000
            this.sessionNum = new Random().Next(7000, 8000);

            //set the timer's interval to 100
            this.timer.Interval = 100;
        }

        public void Send(RtspResponse response)
        {
            try
            {
                response.Session = sessionNum.ToString();
                buffer = new byte[4096];
                buffer = Encoding.UTF8.GetBytes(response.ToString());
                clientSocket.Send(buffer);
            }
            catch (SocketException err)
            {
                Console.WriteLine("Error occurred on accepted socket:" + err.Message + Environment.NewLine + Environment.NewLine);
            }
        }

        public RtspRequest Listen()
        {
            try
            {
                buffer = new byte[4096];
                //server block on read for client's reply
                int rc = clientSocket.Receive(buffer);
                if (rc == 0)
                {
                    return new RtspRequest("Error");
                }

                var str = Encoding.Default.GetString(buffer) + Environment.NewLine + Environment.NewLine;
                return new RtspRequest(str);
            }
            catch (SocketException err)
            {
                return new RtspRequest("Error occurred on accepted socket:" + err.Message + Environment.NewLine + Environment.NewLine);
            }
        }
    }
}
