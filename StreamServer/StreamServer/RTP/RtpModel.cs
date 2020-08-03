using StreamServer.RTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace StreamServer.RTP
{
    public class RtpModel
    {
        //UDP socket to send RTP packets to clients
        private Socket udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        private IPEndPoint clientEndPoint;
        private RtpPacket rtpPacket;

        public RtpModel(string IP, string port)
        {
            //create client end point
            this.clientEndPoint = new IPEndPoint(IPAddress.Parse(IP), Int32.Parse(port));
            //use to packetize the packet
            this.rtpPacket = new RtpPacket();
        }

        public byte[] SendPacket(byte[] videoPacket)
        {
            //packetize (add header) to the video packet
            var packetizedVideoFrame = rtpPacket.createPacket(videoPacket);

            try
            {
                //send the packetized video packet to the client
                udpSocket.SendTo(packetizedVideoFrame, clientEndPoint);
                return packetizedVideoFrame;
            }
            catch (SocketException err)
            {
                Console.WriteLine("SendTo failed: ", err.Message);
                return null;
            }
        }

        //increment packet time
        public void IncrementPacketTime()
        {
            rtpPacket.incrementPacketTime();
        }

    }
}
