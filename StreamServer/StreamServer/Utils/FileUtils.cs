using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamServer.Utils
{
    public static class FileUtils
    {
        public static List<string> GetAllMJPEGFilesFromDir(string path)
        {
            var a = Directory.GetFiles(path, "*.mjpeg");
            var b = a.Select(f => f.Replace(path, string.Empty)).ToList();
            return b;
        }
    }
}
