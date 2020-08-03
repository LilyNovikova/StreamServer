using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamServer.RTSP
{
    public class RtspRequest
    {
        public enum RtspRequestType
        {
            SETUP,
            PLAY,
            PAUSE,
            TEARDOWN,
            DESCRIBE
        }

        public RtspRequestType RequestType { get; set; }
        public string RtspVersion { get; set; } = "RTSP/1.0";
        public string FileName { get; set; }
        public string CSeq { get; set; }
        public string Session { get; set; } = string.Empty;
        public string ClientPort { get; set; }
        public string ServerAddress { get; set; }

        public RtspRequest() { }

        public RtspRequest(string request)
        {
            //split the request message by blank space (both " " and "\r\n" are considered as blank spaces)
            var requestArr = request.Trim().Split(new char[0], StringSplitOptions.RemoveEmptyEntries).Where(str => str != string.Empty).ToArray();
            if (requestArr.Length < 2)
            {
                throw new ArgumentException($"'{request}' is not a RTSP request");
            }
            RequestType = GetRtspRequestType(requestArr[0]);
            RtspVersion = requestArr[2];
            CSeq = requestArr[4];
            if (RequestType != RtspRequestType.DESCRIBE)
            {
                var fileLocArr = requestArr[1].Split('/');
                FileName = fileLocArr[3];
                ServerAddress = requestArr[1].Replace(FileName, string.Empty);

                if (RequestType == RtspRequestType.SETUP)
                {
                    //save the client Port
                    ClientPort = requestArr[8].Trim();
                }
                else
                {
                    Session = requestArr[6].Trim();
                }
            }
        }

        private RtspRequestType GetRtspRequestType(string type)
        {
            switch (type)
            {
                case "SETUP":
                    return RtspRequestType.SETUP;
                case "PLAY":
                    return RtspRequestType.PLAY;
                case "PAUSE":
                    return RtspRequestType.PAUSE;
                case "TEARDOWN":
                    return RtspRequestType.TEARDOWN;
                case "DESCRIBE":
                    return RtspRequestType.DESCRIBE;
                default:
                    throw new ArgumentException($"{type} is not a request type");
            }
        }

        public override string ToString()
        {
            var str = new StringBuilder(Environment.NewLine + "\r\n");
            str.AppendLine($"{ RequestType} {ServerAddress}{FileName} {RtspVersion}");
            str.Append($" {nameof(CSeq)}: {CSeq.Trim()}\r\n");
            if (RequestType != RtspRequestType.SETUP)
            {
                str.Append($" {nameof(Session)}: {Session}\r\n");
            }
            return str.ToString();
        }
    }
}
