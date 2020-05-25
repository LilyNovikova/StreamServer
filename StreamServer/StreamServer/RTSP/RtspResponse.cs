using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamServer.RTSP
{
    public class RtspResponse
    {
        public string RtspVersion { get; set; } = "RTSP/1.0";
        public string Status { get; set; } = "200 OK";
        public string CSeq { get; set; }
        public string Session { get; set; }
        public string Content { get; set; }

        public RtspResponse() { }

        public override string ToString()
        {
            var str = new StringBuilder($"{ RtspVersion} {Status}");
            str.AppendLine($" {nameof(CSeq)}: {CSeq}");
            str.AppendLine($" {nameof(Session)}: {Session}");
            if (Content != null)
            {
                str.AppendLine($" {nameof(Content)}: {Content}");
            }
            return str.ToString();
        }
    }
}
