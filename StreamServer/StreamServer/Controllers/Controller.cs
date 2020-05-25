using StreamServer.Configuration;
using StreamServer.RTP;
using StreamServer.RTSP;
using StreamServer.Utils;
using StreamServer.Video;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using View = StreamServer.Views.View;

namespace StreamServer.Controllers
{
    public class Controller
    {
        private Thread listenThread;
        private static View view;
        private RtspModel rtspModel = null;
        private Thread clientThread;
        private int clientNum = 1;

        public void Listen(object sender, EventArgs e)
        {
            //Determine which view to control
            view = (View)((Button)sender).FindForm();
            view.DisableListenButton();

            //Create a thread to accept from connected clients
            this.listenThread = new Thread(ListenForClients);
            listenThread.IsBackground = true;
            this.listenThread.Start();
        }

        public void IncrementTimer(object sender, EventArgs e, ref RtpModel rtpModel, ref MJPEGVideo video)
        {
            //increment packet timer
            rtpModel.IncrementPacketTime();

            //check if end of file and if it is, then reset file
            if (video.EndofFile())
            {
                video.ResetVideo();
            }

            //get the next frame
            var videoBuffer = video.GetNextFrame();

            //packetize and send the next frame
            var packetizedVideoFrame = rtpModel.SendPacket(videoBuffer);

            //create blank byte array to store header bytes
            var headerBytes = new byte[12];

            //copy first 12 bytes of the packetized video frame into the headerBytes byte array
            System.Buffer.BlockCopy(packetizedVideoFrame, 0, headerBytes, 0, 12);

            //convert the header bytes into bits
            var headerBits = string.Join(" ", headerBytes.Select(x => Convert.ToString(x, 2).PadLeft(8, '0')));
        }

        public void ListenForClients()
        {
            //Create a model to listen from clients
            rtspModel = new RtspModel(int.Parse(view.ServerPort), this.ServerIP);
            //Update the view
            UpdateServerIP(rtspModel.ServerIP.ToString());

            while (true)
            {
                UpdateServerBox("Server is waiting for new connection #" + clientNum + Environment.NewLine);

                //blocks until a client has connected to the server
                var rtspSocket = rtspModel.AcceptOneClient();

                clientNum++;
                //create a thread to handle communication with connected client
                clientThread = new Thread(new ParameterizedThreadStart(Communications));
                clientThread.IsBackground = true; //to stop all threads with application is terminated
                clientThread.Start(rtspSocket);
            }
        }

        public void Communications(object obj)
        {

            //byte[] receiveBuffer = new byte[4096];
            Dictionary<String, String> request2 = new Dictionary<String, String>();

            MJPEGVideo video = null;
            RtpModel rtpModel = null;

            var clientSocket = obj as Socket;
            var clientAddress = clientSocket.RemoteEndPoint;

            //update server box to show that a client has connected
            this.UpdateServerBox("Accepted connection from: "
                + clientSocket.RemoteEndPoint.ToString() + Environment.NewLine);

            var clientModel = new ClientModel(clientSocket);

            //watch for timer to tick and run increment timer function when ticks
            clientModel.timer.Elapsed += new ElapsedEventHandler((sender, e) => IncrementTimer(sender, e, ref rtpModel, ref video));

            while (true)
            {
                RtspRequest request = null;
                try
                {
                    request = clientModel.Listen();
                    UpdateClientBox(request.ToString() + Environment.NewLine);
                }
                catch (ArgumentException)
                {
                    break;
                }

                switch (request.RequestType)
                {
                    case RtspRequest.RtspRequestType.DESCRIBE:
                        this.UpdateServerBox("Client " + clientAddress.ToString()
                        + " has requested description" + Environment.NewLine);
                        clientModel.Send(new RtspResponse
                        {
                            RtspVersion = request.RtspVersion,
                            CSeq = request.CSeq,
                            Content = FileUtils.GetAllMJPEGFilesFromDir(ServerConfig.FilesDirectory)
                            .Aggregate(string.Empty, (res, next) => res += next + ";")
                        });
                        break;
                    case RtspRequest.RtspRequestType.SETUP:
                        this.UpdateServerBox("Client " + clientAddress.ToString()
                        + " has setted up" + Environment.NewLine);
                        rtpModel = new RtpModel(clientAddress.ToString().Split(':')[0], request.ClientPort);
                        //load video
                        video = new MJPEGVideo(request.FileName);
                        break;

                    case RtspRequest.RtspRequestType.PLAY:
                        this.UpdateServerBox("Client " + clientAddress.ToString()
                        + " is playing " + request.FileName + Environment.NewLine);
                        //start the timer
                        clientModel.timer.Start();
                        break;

                    case RtspRequest.RtspRequestType.PAUSE:
                        this.UpdateServerBox("Client " + clientAddress.ToString()
                        + " paused " + request.FileName + Environment.NewLine);
                        //pause the timer
                        clientModel.timer.Stop();
                        break;

                    case RtspRequest.RtspRequestType.TEARDOWN:
                        this.UpdateServerBox("Client " + clientAddress.ToString()
                        + " teared down " + request.FileName + Environment.NewLine);
                        //stop timer
                        clientModel.timer.Stop();
                        //server reply
                        clientModel.Send(new RtspResponse
                        {
                            RtspVersion = request.RtspVersion,
                            CSeq = request.CSeq,
                            Session = request.Session
                        });
                        video = null;
                        rtpModel = null;
                        clientModel = null;
                        clientSocket = obj as Socket;
                        clientModel = new ClientModel(clientSocket);
                        clientModel.timer.Elapsed += new ElapsedEventHandler((sender, e) => IncrementTimer(sender, e, ref rtpModel, ref video));
                        break;
                }
                if (request.RequestType != RtspRequest.RtspRequestType.DESCRIBE)
                {
                    clientModel.Send(new RtspResponse
                    {
                        RtspVersion = request.RtspVersion,
                        CSeq = request.CSeq,
                        Session = request.Session
                    });
                }
            }
        }

        public void UpdateServerIP(string msg)
        {
            view.SetServerIP(msg);
        }

        public IPAddress ServerIP => view.ServerIP;

        public void UpdateServerBox(string msg)
        {
            view.SetServerBox(msg);
        }

        public void UpdateClientBox(string msg)
        {
            view.SetClientBox(msg);
        }
    }
}
